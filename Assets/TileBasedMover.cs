using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBasedMover : MonoBehaviour
{
    public Animator animator;
    public FollowDwarf lampy;
    public MapGen world;

    public float moveSpeed = 1f;
    public float climbingDifficulty = 1.5f;
    public float fallSpeedMultiplier = 1f;
    private bool canMove = true, moving = false, m_FacingRight = true;
    public bool isFalling = false;
    public float setMoveCooldown = 1f;
    private float moveCooldown = 0f;
    private float tileDifficulty = 1f;

    public Vector3 targetPos;

    void Move()
    {
        if (moveCooldown <= 0f)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
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

                    if (world.getTile(targetPos + new Vector3(0f, -1f, 0f)) != null)
                    {
                        //There is a block below the target position, movement is good

                        if (vertical < 0 && world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) == null && world.getTile(targetPos + new Vector3(1f, 0f, 0f)) == null)
                        {
                            //dwarf is falling down one block
                            isFalling = true;
                            fallSpeedMultiplier = 3f;
                            animator.SetBool("isJumping", true);
                        }
                    }
                    else
                    {
                        //There is no block below the target position, check if climbing is possible

                        if (world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) != null && world.getTile(targetPos + new Vector3(1f, 0f, 0f)) != null)
                        {
                            //There is both a block to the left and to the right of the target position, use the direction that the dwarf is facing to climb
                            tileDifficulty = climbingDifficulty;
                        }
                        else if (world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) != null)
                        {
                            //There is a block to the left of the target position, climbing on the left is possible
                            if (m_FacingRight)
                            {
                                Flip();
                            }
                            tileDifficulty = climbingDifficulty;
                        }
                        else if (world.getTile(targetPos + new Vector3(1f, 0f, 0f)) != null)
                        {
                            //There is a block to the right of the target position, climbing on the right is possible
                            if (!m_FacingRight)
                            {
                                Flip();
                            }
                            tileDifficulty = climbingDifficulty;
                        }
                        else
                        {
                            //There is no block on either side of the target position, check if the dwarf can climb up/over or down/under a tile
                            
                            if (vertical > 0)
                            {
                                if (world.getTile(transform.position + new Vector3(-1f, 0f, 0f)) != null && world.getTile(transform.position + new Vector3(1f, 0f, 0f)) != null)
                                {
                                    //There is both a block to the left and to the right of the current position, use facing direction to climb up and over
                                    if (m_FacingRight)
                                    {
                                        targetPos += new Vector3(1f, 0f, 0f);
                                    }
                                    else
                                    {
                                        targetPos += new Vector3(-1f, 0f, 0f);
                                    }
                                    tileDifficulty = climbingDifficulty;
                                }
                                else if (world.getTile(transform.position + new Vector3(-1f, 0f, 0f)) != null)
                                {
                                    //There is a block to the left of the current position, climb up and over to the left
                                    if (m_FacingRight)
                                    {
                                        Flip();
                                    }
                                    targetPos += new Vector3(-1f, 0f, 0f);
                                    tileDifficulty = climbingDifficulty;
                                }
                                else if (world.getTile(transform.position + new Vector3(1f, 0f, 0f)) != null)
                                {
                                    //There is a block to the right of the current position, climb up and over to the right
                                    if (!m_FacingRight)
                                    {
                                        Flip();
                                    }
                                    targetPos += new Vector3(1f, 0f, 0f);
                                    tileDifficulty = climbingDifficulty;
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
                                if (m_FacingRight)
                                {
                                    Flip();
                                }

                                if (world.getTile(transform.position + new Vector3(0f, -1f, 0f)) != null)
                                {
                                    //There is a block below the current position, climb down and to the left
                                    targetPos += new Vector3(0f, -1f, 0f);
                                    tileDifficulty = climbingDifficulty;
                                }
                                else if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) != null)
                                {
                                    //There is a block above the target position, climb along the ceiling
                                    tileDifficulty = climbingDifficulty;
                                }
                                else if (world.getTile(transform.position + new Vector3(0f, 1f, 0f)) != null)
                                {
                                    //There is a block above the current position, climb around it
                                    targetPos += new Vector3(0f, 1f, 0f);
                                    tileDifficulty = climbingDifficulty;
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
                                if (!m_FacingRight)
                                {
                                    Flip();
                                }

                                if (world.getTile(transform.position + new Vector3(0f, -1f, 0f)) != null)
                                {
                                    //There is a block below the current position, climb down and to the right
                                    targetPos += new Vector3(0f, -1f, 0f);
                                    tileDifficulty = climbingDifficulty;
                                }
                                else if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) != null)
                                {
                                    //There is a block above the target position, climb along the ceiling
                                    tileDifficulty = climbingDifficulty;
                                }
                                else if (world.getTile(transform.position + new Vector3(0f, 1f, 0f)) != null)
                                {
                                    //There is a block above the current position, climb around it
                                    targetPos += new Vector3(0f, 1f, 0f);
                                    tileDifficulty = climbingDifficulty;
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
                                fallSpeedMultiplier = 3f;
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
                    }
                }
                else
                {
                    //Digging attempted
                    bool validDig = true;

                    if (world.getTile(targetPos + new Vector3(-1f, 0f, 0f)) == null && world.getTile(targetPos + new Vector3(1f, 0f, 0f)) == null)
                    {
                        if (world.getTile(targetPos + new Vector3(0f, -1f, 0f)) == null)
                        {
                            if (vertical > 0)
                            {
                                //climb around block depending on direction facing
                                if (m_FacingRight)
                                {
                                    targetPos += new Vector3(1f, 0f, 0f);
                                    tileDifficulty = climbingDifficulty;

                                    validDig = false;
                                }
                                else
                                {
                                    targetPos += new Vector3(-1f, 0f, 0f);
                                    tileDifficulty = climbingDifficulty;

                                    validDig = false;
                                }
                            }
                            else if (vertical < 0)
                            {
                                //digging is possible, but player will fall after digging
                                isFalling = true;
                                fallSpeedMultiplier = 1f;
                                animator.SetBool("isJumping", true);

                            }
                            else if (horizontal < 0)
                            {
                                //digging is not possible, but player can climb over/under
                                if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) == null)
                                {
                                    targetPos += new Vector3(0f, 1f, 0f);
                                }
                                else
                                {
                                    targetPos += new Vector3(0f, -1f, 0f);
                                }
                                
                                tileDifficulty = climbingDifficulty;

                                validDig = false;
                            }
                            else if (horizontal > 0)
                            {
                                //digging is not possible, but player can climb over/under
                                if (world.getTile(targetPos + new Vector3(0f, 1f, 0f)) == null)
                                {
                                    targetPos += new Vector3(0f, 1f, 0f);
                                }
                                else
                                {
                                    targetPos += new Vector3(0f, -1f, 0f);
                                }

                                tileDifficulty = climbingDifficulty;

                                validDig = false;
                            }
                            
                        }
                    }

                    if (validDig)
                    {
                        if (world.getTile(targetPos) != null)
                        {
                            tileDifficulty = world.getTile(targetPos).GetComponent<TileDifficulty>().difficulty;
                        }
                        //Diggin Occurs
                        Destroy(moveTile);
                    }
                    
                }

                animator.SetFloat("speed", moveSpeed);

                moveCooldown = setMoveCooldown;
                
            }
            
        }
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moveCooldown--;

        if (canMove)
        {
            targetPos = transform.position;
            Move();
        }
        if (moving)
        {
            if (transform.position == targetPos)
            {
                tileDifficulty = 1f;

                if (!isFalling)
                {
                    animator.SetFloat("speed", 0f);
                    animator.SetBool("isJumping", false);

                    moving = false;
                    canMove = true;
                }
                else
                {
                    if (world.getTile(targetPos + new Vector3(0f, -1f, 0f)) != null)
                    {
                        isFalling = false;
                        fallSpeedMultiplier = 1f;
                    }
                    else
                    {
                        targetPos += new Vector3(0f, -1f, 0f);
                        fallSpeedMultiplier += 2f;
                    }
                }
                

            }

            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed * fallSpeedMultiplier / tileDifficulty);
        }
        else
        {
            
        }
    }
}
