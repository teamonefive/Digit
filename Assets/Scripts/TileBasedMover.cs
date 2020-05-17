using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileBasedMover : MonoBehaviour
{
    public Animator animator;
    public FollowDwarf lampy;
    public MapGen world;
    public Stats stat;
    public GameObject debris;
    //public GameObject depthValue;
    public GameObject pickaxeSlot;
    public AudioSource audioSource;
    public AudioClip digSound;
    public AudioClip moveSound;
    public Button moveUp;
    public Button moveDown;
    public Button moveRight;
    public Button moveLeft;
    public Button moveRight1;
    public Button moveLeft1;

    public bool canMove = true, moving = false, m_FacingRight = true;
    public bool isFalling = false;
    public bool isDestroyed = false;
    public bool isDestroyedBlock = false;
    public bool isDigging = false;
    public bool isFatigued = false;
    public Vector2 dir = new Vector2(0f, 0f);

    private Vector2 touchOrigin = -Vector2.one;

    public Vector3 oldPos;
    public Vector3 targetPos;

    [SerializeField]
    private Item1[] items;

    public GameObject broken;

    private bool pickaxeUsed = false;

    public Slider durabilityBar;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        //fatigue = GetComponent<Fatigue>();

        #if UNITY_STANDALONE || UNITY_WEBPLAYER
        return;
        #endif

        Button up = moveUp.GetComponent<Button>();
        up.onClick.AddListener(goUp);
        Button down = moveDown.GetComponent<Button>();
        down.onClick.AddListener(goDown);
        Button right = moveRight.GetComponent<Button>();
        right.onClick.AddListener(goRight);
        Button left = moveLeft.GetComponent<Button>();
        left.onClick.AddListener(goLeft);
        Button right1 = moveRight1.GetComponent<Button>();
        right1.onClick.AddListener(goRight);
        Button left1 = moveLeft1.GetComponent<Button>();
        left1.onClick.AddListener(goLeft);

    }

    private void Awake()
    {
        oldPos = new Vector3(-53.5f, -1.16f, 0f);
        targetPos = oldPos;
        stat.width = Screen.width / 2.0f;
        stat.height = Screen.height / 2.0f;
    }

    public void goUp()
    {
        print("UP");
        dir = new Vector2(0f, 1f);
        return;
    }

    public void goDown()
    {
        print("DOWN");
        dir = new Vector2(0f, -1f);
        return;
    }
    public void goRight()
    {
        print("RIGHT");
        dir = new Vector2(1f, 0f);
        return;
    }
    public void goLeft()
    {
        print("LEFT");
        dir = new Vector2(-1f, 0f);
        return;
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
    public Vector2 touchLocation()
    {
        //Vector2 touchLoc = new Vector2(0f, 0f);
        Vector2 touchLoc = dir;

        #if UNITY_STANDALONE || UNITY_WEBPLAYER

        touchLoc.x = Input.GetAxisRaw("Horizontal");
        touchLoc.y = Input.GetAxisRaw("Vertical");

        #endif 
        return touchLoc;
    }

    void Move()
    {
        
        Vector2 touchLoc = touchLocation();
        if (stat.moveCooldown >= 0f) return;


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

        if (!moveTile)
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
                    stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                }
                else if (world.getTile(targetPos + new Vector3(1f, 0f, 0f)) != null)
                {
                    //There is a block to the right of the target position, climbing on the right is possible
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
                            oldPos = targetPos;
                            world.renderUp(oldPos);
                            targetPos += new Vector3(-1f, 0f, 0f);
                            stat.climbingDifficultyMultiplier = stat.climbingDifficulty;
                        }
                        else if (isGround(transform.position + new Vector3(1f, 0f, 0f)))
                        {
                            //There is a block to the right of the current position, climb up and over to the right
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
                        
                        if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) != null)
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
                        else if (world.getTile(transform.position + new Vector3(0f, -1f, 0f)) != null)
                        {
                            //There is a block below the current position, climb down and to the left
                            oldPos = targetPos;
                            world.renderLeft(oldPos);
                            targetPos += new Vector3(0f, -1f, 0f);
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
                        
                        if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) != null)
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
                        else if (world.getTile(transform.position + new Vector3(0f, -1f, 0f)) != null)
                        {
                            //There is a block below the current position, climb down and to the right
                            oldPos = targetPos;
                            world.renderRight(oldPos);
                            targetPos += new Vector3(0f, -1f, 0f);
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

            if (!moveTile.GetComponent<Tile>().isBreakable)
            {
                validDig = false;
                moving = false;
                canMove = true;
                return;
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

            SlotScript slot = pickaxeSlot.GetComponent<SlotScript>();
            Item1 item = slot.MyItem;

            if (item == null)
            {
                validDig = false;
                if (moveTile.GetComponent<Tile>().type != Tile.TileType.Water)
                {
                    moving = false;
                    canMove = true;
                    return;
                }
            }
            else if (item.MyTitle != "Wood Pickaxe" && item.MyTitle != "Iron Pickaxe" && item.MyTitle != "Silver Pickaxe" && item.MyTitle != "Gold Pickaxe" && item.MyTitle != "Mithril Pickaxe")
            {
                validDig = false;
                if (moveTile.GetComponent<Tile>().type != Tile.TileType.Water)
                {
                    moving = false;
                    canMove = true;
                    return;
                }
            }

            if (validDig)
            {
                //animator.SetBool("isDigging", true);
                animator.Play("dwarf_dig");
                audioSource.PlayOneShot(digSound, 1.0f);
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
                        stat.totalIron++;
                        break;
                    case Tile.Treasure.Silver:
                        OreSilver silver = (OreSilver)Instantiate(items[1]);
                        InventoryScript.MyInstance.AddItem(silver);
                        stat.totalSilver++;
                        break;
                    case Tile.Treasure.Gold:
                        OreGold gold = (OreGold)Instantiate(items[2]);
                        InventoryScript.MyInstance.AddItem(gold);
                        stat.totalGold++;
                        break;
                    case Tile.Treasure.Mithril:
                        OreMithril mithril = (OreMithril)Instantiate(items[3]);
                        InventoryScript.MyInstance.AddItem(mithril);
                        stat.totalMithril++;
                        break;
                    case Tile.Treasure.Topaz:
                        GemTopaz topaz = (GemTopaz)Instantiate(items[4]);
                        InventoryScript.MyInstance.AddItem(topaz);
                        stat.totalTopaz++;
                        break;
                    case Tile.Treasure.Sapphire:
                        GemSapphire sapphire = (GemSapphire)Instantiate(items[5]);
                        InventoryScript.MyInstance.AddItem(sapphire);
                        stat.totalSapphire++;
                        break;
                    case Tile.Treasure.Ruby:
                        GemRuby ruby = (GemRuby)Instantiate(items[6]);
                        InventoryScript.MyInstance.AddItem(ruby);
                        stat.totalRuby++;
                        break;
                    case Tile.Treasure.Diamond:
                        GemDiamond diamond = (GemDiamond)Instantiate(items[7]);
                        InventoryScript.MyInstance.AddItem(diamond);
                        stat.totalDiamond++;
                        break;
                    default:
                        break;

                }

                Destroy(moveTile);
                //TODO:Jon Broken tiles need to be part of the tile prefabs as a background.
                //         Instead of destroy we need a function to toggle off front tile.
                GameObject brokenTexture = Instantiate(broken, targetPos, Quaternion.identity);
                Instantiate(debris, targetPos, Quaternion.identity);

                world.activeTiles[world.tilePos(targetPos)] = null;
                world.brokenTiles.Add(world.tilePos(targetPos), brokenTexture);

                isDestroyed = true;
                isDestroyedBlock = true;
                pickaxeUsed = true;

                stat.totalDigs++;

                //expand liquids if need be
                world.expandLiquid(world.tilePos(new Vector2(targetPos.x, targetPos.y + 1f)));
                world.expandLiquid(world.tilePos(new Vector2(targetPos.x - 1f, targetPos.y)));
                world.expandLiquid(world.tilePos(new Vector2(targetPos.x + 1f, targetPos.y)));
                //animator.SetBool("isDigging", false);
            }
            

        }

        if (stat.climbingDifficultyMultiplier < 1f)
        {
            stat.climbingDifficultyMultiplier = 1f;
        }

        animator.SetFloat("speed", stat.moveSpeed);

        stat.moveCooldown = stat.setMoveCooldown;
        dir = new Vector2(0f, 0f);
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
        //if (Input.touchCount > 0)
        //{
        //    stat.touch = Input.GetTouch(0);
        //    print("Touch Position : " + stat.touch.position);
        //    if (stat.touch.phase == TouchPhase.Began)
        //    {
        //        oldPos = stat.touch.position;
        //    }

            //if (stat.touch.phase == TouchPhase.Ended)
            //{
            //    dir = new Vector2(0f, 0f);
            //}
        //}



        SlotScript slot = pickaxeSlot.GetComponent<SlotScript>();
        Item1 item = slot.MyItem;
        if (item != null)
        {
            durabilityBar.maxValue = item.myMaxDurability;
            durabilityBar.value = item.myDurability;
        }
        else
        {
            durabilityBar.value = 0;
        }
        

        stat.moveCooldown--;


        if (canMove)
        {
            oldPos = transform.position;
            targetPos = transform.position;
            Move();
        }
        if (moving)
        {
            if (!audioSource.isPlaying) 
            {
                audioSource.PlayOneShot(moveSound, 1.0f);
            }
            if (transform.position == targetPos)
            {
                stat.tileDifficultyMultiplier = 1f;
                stat.climbingDifficultyMultiplier = 1f;
                stat.totalMoves++;

                Vector2 tilePos = world.tilePos(targetPos);
                stat.currentDepth = (int)tilePos.y;

                string txt = "";
                txt += (stat.currentDepth - 49);
                //depthValue.GetComponent<Text>().text = txt;

                if (stat.currentDepth > stat.maxDepth)
                {
                    stat.maxDepth = stat.currentDepth;
                }

                if (!isFalling)
                {
                    animator.SetFloat("speed", 0f);
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isFalling", false);

                    //update fatigue
                    if (isDestroyedBlock)
                    {
                        stat.vFatigue -= 10f;
                        isDestroyedBlock = false;
                    }
                    else
                    {
                        stat.vFatigue -= 2.5f;
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
                            animator.SetBool("TransitionFatigue", true);
                            world.UnrenderAllTiles();
                            Vector3 dwarfPos = new Vector3(-53.5f, -1f, 0f);
                            oldPos = dwarfPos;
                            targetPos = dwarfPos;
                            Vector3 lampPos = new Vector3(-53.1f, -0.55f, 0f);

                            stat.totalDeaths++;

                            world.generateStartingTiles();
                            lampy.snapToOrigin();

                            moving = false;
                            canMove = true;
                            return;
                        }
                    }

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

                world.saveManager.createSave();
            }

            float pickaxeMultiplier = 1f;
            if (!slot.IsEmpty)
            {

                if (item.MyTitle == "Wood Pickaxe")
                {
                    pickaxeMultiplier = 1f;

                    if (pickaxeUsed)
                    {
                        item.myDurability = item.myDurability - 1;

                        if (item.myDurability <= 0)
                        {
                            slot.Clear();
                            stat.itemsBroken++;
                        }
                    }

                }
                else if (item.MyTitle == "Iron Pickaxe")
                {
                    pickaxeMultiplier = 1.1f;

                    if (pickaxeUsed)
                    {
                        item.myDurability = item.myDurability - 1;

                        if (item.myDurability <= 0)
                        {
                            slot.Clear();
                            stat.itemsBroken++;
                        }
                    }

                }
                else if (item.MyTitle == "Silver Pickaxe")
                {
                    pickaxeMultiplier = 1.2f;

                    if (pickaxeUsed)
                    {
                        item.myDurability = item.myDurability - 1;

                        if (item.myDurability <= 0)
                        {
                            slot.Clear();
                            stat.itemsBroken++;
                        }
                    }
                }
                else if (item.MyTitle == "Gold Pickaxe")
                {
                    pickaxeMultiplier = 1.35f;

                    if (pickaxeUsed)
                    {
                        item.myDurability = item.myDurability - 1;

                        if (item.myDurability <= 0)
                        {
                            slot.Clear();
                            stat.itemsBroken++;
                        }
                    }
                }
                else if (item.MyTitle == "Mithril Pickaxe")
                {
                    pickaxeMultiplier = 1.5f;

                    if (pickaxeUsed)
                    {
                        item.myDurability = item.myDurability - 1;

                        if (item.myDurability <= 0)
                        {
                            slot.Clear();
                            stat.itemsBroken++;
                        }
                    }
                }

            }
            pickaxeUsed = false;

            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * stat.moveSpeed * pickaxeMultiplier * stat.fallSpeedMultiplier / stat.tileDifficultyMultiplier / stat.climbingDifficultyMultiplier);
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

    public void teleportToSpawn()
    {
        targetPos = oldPos;
        transform.position = oldPos;
        animator.SetFloat("speed", 0f);
        world.UnrenderAllTiles();
        Vector3 dwarfPos = new Vector3(-53.5f, -1f, 0f);
        oldPos = dwarfPos;
        targetPos = dwarfPos;

        world.generateStartingTiles();
        lampy.snapToOrigin();

        moving = false;
        canMove = true;
        return;
    }
}

