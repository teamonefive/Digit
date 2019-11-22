using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDwarf : MonoBehaviour
{
    public TileBasedMover dwarfMover;

    private Vector2 pos;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float speedMultiplier = 1f;

    void Start() 
    {

    }
    void Update()
    {
        pos = dwarfMover.targetPos + new Vector3(xOffset, yOffset, 0f);

        if (pos.x != transform.position.x)
        {
            speedMultiplier = 2f;
        }
        else
        {
            bool noneOfTheAbove = true;

            //this smooths out lampy's movement if the dwarf is falling

            if (transform.position.y - pos.y > 3)
            {
                speedMultiplier = 3f;
                noneOfTheAbove = false;
            }
            if (transform.position.y - pos.y > 6)
            {
                speedMultiplier = 5f;
            }
            if (transform.position.y - pos.y > 10)
            {
                speedMultiplier = 100f;
            }
            
            if (noneOfTheAbove)
            {
                speedMultiplier = 1.2f;
            }
            
        }

        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * dwarfMover.moveSpeed * speedMultiplier);
        
    }
}
