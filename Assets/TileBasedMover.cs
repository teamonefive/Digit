using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBasedMover : MonoBehaviour
{
    public Animator animator;
    public FollowDwarf lampy;
    public MapGen world;
    public Stats stat;
    private Fatigue fatigue;
    public GameObject debris;
    //Inventory playerInventory;

    private bool canMove = true, moving = false, m_FacingRight = true;
    public bool isFalling = false;
    public bool isDestroyed = false;
    public bool isDestroyedBlock = false;
    public bool isDigging = false;
    public bool isFatigued = false;


    private Vector2 touchOrigin = -Vector2.one;

    public Vector3 oldPos;
    public Vector3 targetPos;

    [SerializeField]
    private Item1[] items;

    private void Awake()
    {
        //playerInventory = this.GetComponent<Inventory>();
        fatigue = this.GetComponent<Fatigue>();
    }

    bool isGround(Vector2 pos)
    {
        //todo:jon set single true/false case
        if (world.getTile(pos) == null)
        {
            animator.SetBool("isGrounded", false);
            return false;
        }
        else if (world.getTile(pos).GetComponent<Tile>().type == Tile.TileType.Water 
              || world.getTile(pos).GetComponent<Tile>().type == Tile.TileType.Lava)
        { 
            animator.SetBool("isGrounded", false);
            return false;
        }
        else
        {
            animator.SetBool("isGrounded", true);
            return true;
        }
    }

    /*
    Returns the Vector2 direction of the touch/click.
    */
    Vector2 touchLocation()
    {
        Vector2 touchLoc = new Vector2(0f, 0f);

        #if UNITY_STANDALONE || UNITY_WEBPLAYER

        touchLoc.x = Input.GetAxisRaw("Horizontal");
        touchLoc.y = Input.GetAxisRaw("Vertical");

        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (Input.touchCount == 0) return touchLoc;

        Touch myTouch = Input.touches[0];

        //Check if the phase of that touch equals Began
        if (myTouch.phase == TouchPhase.Began)
        {
            //If so, set touchOrigin to the position of that touch
            touchOrigin = myTouch.position;
        }

        //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
        else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
        {
            //Set touchEnd to equal the position of this touch
            Vector2 touchEnd = myTouch.position;

            //Calculate the difference between the beginning and end of the touch on the x axis.
            float x = touchOrigin.x - Screen.width / 2;

            //Calculate the difference between the beginning and end of the touch on the y axis.
            float y = touchOrigin.y - Screen.height / 2;

            //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
            touchOrigin.x = -1;

            //Check if the difference along the x axis is greater than the difference along the y axis.
            if (Mathf.Abs(x) > Mathf.Abs(y))
                //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                touchLoc.x = x > 0 ? 1 : -1;
            else
                //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                touchLoc.y = y > 0 ? 1 : -1;
        }

        #endif //End of mobile platform dependendent compilation section started above with #elif

        return touchLoc;
    }

    void Move()
    {
        if (stat.moveCooldown >= 0f) return;

        Vector2 touchLoc = touchLocation();
        float horizontal = touchLoc.x;
        float vertical = touchLoc.y;
        if (horizontal == 0 && vertical == 0) return;

        moving = true;
        canMove = false;

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (m_FacingRight && horizontal < 0 || !m_FacingRight && horizontal > 0)
        {
            Flip();
        }

        targetPos += new Vector3(horizontal, vertical, 0f);

        GameObject moveTile = world.getTile(targetPos);

        if (moveTile == null)
        {
            //Dwarf is moving into empty space

            if (isGround(targetPos + new Vector3(0f, -1f, 0f)))
            {
                //There is a block below the target position, movement is good
                if (vertical < 0 && world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) == null 
                                 && world.getTile(targetPos + new Vector3( 1f, 0f, 0f)) == null)
                {
                    //dwarf is falling down one block
                    isFalling = true;
                    stat.fallSpeedMultiplier = 3f;
                    animator.SetBool("isFalling", true);
                }
            }
            else
            {
                //There is no block below the target position, check if climbing is possible

                if (world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) != null && world.getTile(targetPos + new Vector3(1f, 0f, 0f)) != null)
                {
                    //There is both a block to the left and to the right of the target position, use the direction that the dwarf is facing to climb
                    stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                }
                else if (world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) != null)
                {
                    //There is a block to the left of the target position, climbing on the left is possible
                    /*
                    if (m_FacingRight)
                    {
                        Flip();
                    }
                    */
                    stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                }
                else if (world.getTile(targetPos + new Vector3(1f, 0f, 0f)) != null)
                {
                    //There is a block to the right of the target position, climbing on the right is possible
                    /*
                    if (!m_FacingRight)
                    {
                        Flip();
                    }
                    */
                    stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                }
                else
                {
                    //There is no block on either side of the target position, check if the dwarf can climb up/over or down/under a tile
                    
                    if (vertical > 0)
                    {
                        if (isGround(transform.position + new Vector3(-1f, 0f, 0f)) && isGround(transform.position + new Vector3(1f, 0f, 0f)))
                        {
                            //There is both a block to the left and to the right of the current position, use facing direction to climb up and over
                            if (m_FacingRight)
                            {
                                oldPos = targetPos;
                                world.renderUp(oldPos);
                                targetPos += new Vector3(1f, 0f, 0f);
                            }
                            else
                            {
                                oldPos = targetPos;
                                world.renderUp(oldPos);
                                targetPos += new Vector3(-1f, 0f, 0f);
                            }
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (isGround(transform.position + new Vector3(-1f, 0f, 0f)))
                        {
                            //There is a block to the left of the current position, climb up and over to the left
                            /*
                            if (m_FacingRight)
                            {
                                Flip();
                            }
                            */
                            oldPos = targetPos;
                            world.renderUp(oldPos);
                            targetPos += new Vector3(-1f, 0f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (isGround(transform.position + new Vector3(1f, 0f, 0f)))
                        {
                            //There is a block to the right of the current position, climb up and over to the right
                            /*
                            if (!m_FacingRight)
                            {
                                Flip();
                            }
                            */
                            oldPos = targetPos;
                            world.renderUp(oldPos);
                            targetPos += new Vector3(1f, 0f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) != null)
                        {
                            //There is a block above the target position, dwarf can jump and attach to ceiling
                            animator.SetBool("isJumping", true);
                        }
                        else
                        {
                            //movement not possible
                            moving = false;
                            canMove = true;

                            return;
                        }
                    }
                    else if (horizontal < 0)
                    {
                        /*
                        if (m_FacingRight)
                        {
                            Flip();
                        }
                        */

                        if (world.getTile(transform.position + new Vector3(0f, -1f, 0f)) != null)
                        {
                            //There is a block below the current position, climb down and to the left
                            oldPos = targetPos;
                            world.renderLeft(oldPos);
                            targetPos += new Vector3(0f, -1f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) != null)
                        {
                            //There is a block above the target position, climb along the ceiling
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (world.getTile(transform.position + new Vector3(0f, 1f, 0f)) != null)
                        {
                            //There is a block above the current position, climb around it
                            oldPos = targetPos;
                            world.renderLeft(oldPos);
                            targetPos += new Vector3(0f, 1f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else
                        {
                            //movement not possible
                            moving = false;
                            canMove = true;

                            return;
                        }
                    }
                    else if (horizontal > 0)
                    {
                        /*
                        if (!m_FacingRight)
                        {
                            Flip();
                        }
                        */

                        if (world.getTile(transform.position + new Vector3(0f, -1f, 0f)) != null)
                        {
                            //There is a block below the current position, climb down and to the right
                            oldPos = targetPos;
                            world.renderRight(oldPos);
                            targetPos += new Vector3(0f, -1f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) != null)
                        {
                            //There is a block above the target position, climb along the ceiling
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (world.getTile(transform.position + new Vector3(0f, 1f, 0f)) != null)
                        {
                            //There is a block above the current position, climb around it
                            oldPos = targetPos;
                            world.renderRight(oldPos);
                            targetPos += new Vector3(0f, 1f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else
                        {
                            //movement not possible
                            moving = false;
                            canMove = true;

                            return;
                        }
                    }
                    else if (vertical < 0)
                    {
                        //dwarf is falling
                        isFalling = true;
                        stat.fallSpeedMultiplier = 3f;
                        animator.SetBool("isFalling", true);
                    }
                    else
                    {
                        //movement not possible
                        moving = false;
                        canMove = true;

                        return;
                    }

                }
            }
        }
        else
        {
            //Digging attempted
            bool validDig = true;

            if (moveTile.GetComponent<Tile>().type == Tile.TileType.Water)
            {
                //Digging is not possible but swimming is
                validDig = false;
            }
            else if (world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) == null && world.getTile(targetPos + new Vector3(1f, 0f, 0f)) == null)
            {
                if (world.getTile(targetPos + new Vector3(0f, -1f, 0f)) == null)
                {
                    if (vertical > 0)
                    {
                        //climb around block depending on direction facing if empty space permits climbing around
                        if (m_FacingRight && world.getTile(oldPos + new Vector3(1f, 0f, 0f)) == null)
                        {
                            oldPos = targetPos;
                            world.renderUp(oldPos);
                            targetPos += new Vector3(1f, 0f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;

                            validDig = false;
                        }
                        else if (!m_FacingRight && world.getTile(oldPos + new Vector3(-1f, 0f, 0f)) == null)
                        {
                            oldPos = targetPos;
                            world.renderUp(oldPos);
                            targetPos += new Vector3(-1f, 0f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;

                            validDig = false;
                        }
                        else
                        {
                            //movement not possible
                            moving = false;
                            canMove = true;

                            return;
                        }
                    }
                    else if (vertical < 0)
                    {
                        //digging is possible, but player will fall after digging
                        isFalling = true;
                        stat.fallSpeedMultiplier = 1f;
                        animator.SetBool("isFalling", true);

                    }
                    else if (horizontal < 0)
                    {
                        //digging is not possible, but player can climb over/under
                        if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) == null && world.getTile(oldPos + new Vector3(0f, 1f, 0f)) == null)
                        {
                            oldPos = targetPos;
                            world.renderLeft(oldPos);
                            targetPos += new Vector3(0f, 1f, 0f);
                        }
                        else
                        {
                            oldPos = targetPos;
                            world.renderLeft(oldPos);
                            targetPos += new Vector3(0f, -1f, 0f);
                        }
                        
                        stat.climbingDifficultyMultiplier = stat.climbingDifficulty;

                        validDig = false;
                    }
                    else if (horizontal > 0)
                    {
                        //digging is not possible, but player can climb over/under
                        if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) == null && world.getTile(oldPos + new Vector3(0f, 1f, 0f)) == null)
                        {
                            oldPos = targetPos;
                            world.renderRight(oldPos);
                            targetPos += new Vector3(0f, 1f, 0f);
                        }
                        else
                        {
                            oldPos = targetPos;
                            world.renderRight(oldPos);
                            targetPos += new Vector3(0f, -1f, 0f);
                        }

                        stat.climbingDifficultyMultiplier = stat.climbingDifficulty;

                        validDig = false;
                    }
                    
                }
            }
            else if (world.getTile(targetPos + new Vector3(0f, -1f, 0f)) == null || vertical != 0)
            {
                stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
            }

            if (validDig)
            {
                if (world.getTile(targetPos) != null)
                {
                    stat.tileDifficultyMultiplier = world.getTile(targetPos).GetComponent<Tile>().difficulty * stat.strengthMultiplier;
                    if (stat.tileDifficultyMultiplier < 1f)
                    {
                        stat.tileDifficultyMultiplier = 1f;
                    }
                }

                isGround(targetPos + new Vector3(0f, -1f, 0f));
                /*
                if (world.getTile(targetPos + new Vector3(0f, -1f, 0f)) != null)
                {
                    animator.SetBool("isGrounded", true);
                }
                else
                {
                    animator.SetBool("isGrounded", false);
                }
                */

                //Diggin Occurs
                world.destroyedTiles.Add(new Vector2((int)(targetPos.x + 70.5), (int)targetPos.y * -1 + 48), false);

                //Add treasure to player inventory
                switch (moveTile.GetComponent<Tile>().treasure)
                {
                    
                    case Tile.Treasure.Iron:
                        OreIron iron = (OreIron)Instantiate(items[0]);
                        InventoryScript.MyInstance.AddItem(iron);
                        break;
                    case Tile.Treasure.Silver:
                        OreSilver silver = (OreSilver)Instantiate(items[1]);
                        InventoryScript.MyInstance.AddItem(silver);
                        break;
                    case Tile.Treasure.Gold:
                        OreGold gold = (OreGold)Instantiate(items[2]);
                        InventoryScript.MyInstance.AddItem(gold);
                        break;
                    case Tile.Treasure.Mithril:
                        OreMithril mithril = (OreMithril)Instantiate(items[3]);
                        InventoryScript.MyInstance.AddItem(mithril);
                        break;
                    case Tile.Treasure.Topaz:
                        GemTopaz topaz = (GemTopaz)Instantiate(items[4]);
                        InventoryScript.MyInstance.AddItem(topaz);
                        break;
                    case Tile.Treasure.Sapphire:
                        GemSapphire sapphire = (GemSapphire)Instantiate(items[5]);
                        InventoryScript.MyInstance.AddItem(sapphire);
                        break;
                    case Tile.Treasure.Ruby:
                        GemRuby ruby = (GemRuby)Instantiate(items[6]);
                        InventoryScript.MyInstance.AddItem(ruby);
                        break;
                    case Tile.Treasure.Diamond:
                        GemDiamond diamond = (GemDiamond)Instantiate(items[7]);
                        InventoryScript.MyInstance.AddItem(diamond);
                        break;
                    default:
                        break;

                }

                Destroy(moveTile);
                Instantiate(debris, targetPos, Quaternion.identity);
                world.activeTiles[world.tilePos(targetPos)] = null;
                isDestroyed = true;
                isDestroyedBlock = true;

                //expand liquids if need be
                world.expandLiquid(world.tilePos(new Vector2(targetPos.x - 1f, targetPos.y)));
                world.expandLiquid(world.tilePos(new Vector2(targetPos.x + 1f, targetPos.y)));
                world.expandLiquid(world.tilePos(new Vector2(targetPos.x, targetPos.y + 1f)));


            }
            

        }

        if (stat.climbingDifficultyMultiplier < 1f)
        {
            stat.climbingDifficultyMultiplier = 1f;
        }

        animator.SetFloat("speed", stat.moveSpeed);

        stat.moveCooldown = stat.setMoveCooldown;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        lampy.xOffset *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        stat.moveCooldown--;

        if (canMove)
        {
            oldPos = transform.position;
            targetPos = transform.position;
            Move();
        }
        if (moving)
        {
            if (transform.position == targetPos)
            {
                stat.tileDifficultyMultiplier = 1f;
                stat.climbingDifficultyMultiplier = 1f;

                if (!isFalling)
                {
                    animator.SetFloat("speed", 0f);
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isFalling", false);

                    //update fatigue
                    if (isDestroyedBlock)
                    {
                        fatigue.updateFatigue(10f);
                        isDestroyedBlock = false;
                    }
                    else
                    {
                        fatigue.updateFatigue(2.5f);
                    }

                    //check for death
                    GameObject currentTile = world.getTile(transform.position);
                    if (currentTile != null)
                    {
                        if (currentTile.GetComponent<Tile>().type == Tile.TileType.Lava)
                        {
                            stat.vFatigue = 0f;
                        }
                        if ((currentTile.GetComponent<Tile>().type == Tile.TileType.Lava || currentTile.GetComponent<Tile>().type == Tile.TileType.Water) && stat.vFatigue < 1f)
                        {
                            world.UnrenderAllTiles();
                            Vector3 dwarfPos = new Vector3(-53.5f, -1f, 0f);
                            Vector3 lampPos = new Vector3(-53.1f, -0.55f, 0f);
                            fatigue.checkFatigue();
                            
                            world.generateStartingTiles();
                            lampy.snapToOrigin();

                            moving = false;
                            canMove = true;
                            return;
                        }
                    }

                    fatigue.checkFatigue();

                    moving = false;
                    canMove = true;

                    
                }
                else
                {
                    if (isGround(targetPos + new Vector3(0f, -1f, 0f)) || world.getTile(targetPos) != null)
                    {
                        isFalling = false;
                        stat.fallSpeedMultiplier = 1f;
                    }
                    else
                    {
                        targetPos += new Vector3(0f, -1f, 0f);
                        stat.fallSpeedMultiplier += 2f;
                    }
                }
                

            }

            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * stat.moveSpeed * stat.fallSpeedMultiplier / stat.tileDifficultyMultiplier / stat.climbingDifficultyMultiplier);
            if (transform.position.x >= oldPos.x + 1)
            {
                world.renderRight(transform.position);
                oldPos = transform.position;
            }
            if (transform.position.x <= oldPos.x - 1)
            {
                world.renderLeft(transform.position);
                oldPos = transform.position;
            }
            if (transform.position.y >= oldPos.y + 1)
            {
                world.renderUp(transform.position);
                oldPos = transform.position;
            }
            if (transform.position.y <= oldPos.y - 1)
            {
                world.renderDown(transform.position);
                oldPos = transform.position;
            }
        }
        else
        {
            
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(6);
    }
}

