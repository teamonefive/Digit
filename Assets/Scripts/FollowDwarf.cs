using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDwarf : MonoBehaviour
{
    public TileBasedMover dwarfMover;
    public Stats stat;

    private Vector2 pos;
    public float xOffset = 0f;
    public float yOffset = 0f;

    void Start() 
    {

    }

    public void snapToOrigin()
    {
        transform.position = new Vector3(-53.1f, -0.55f, 0f);
    }
    void Update()
    {
        pos = dwarfMover.targetPos + new Vector3(xOffset, yOffset, 0f);

        if (pos.x != transform.position.x)
        {
            stat.speedMultiplier = 2f;
        }
        else
        {
            bool noneOfTheAbove = true;

            //this smooths out lampy's movement if the dwarf is falling

            if (transform.position.y - pos.y > 3)
            {
                stat.speedMultiplier = 3f;
                noneOfTheAbove = false;
            }
            if (transform.position.y - pos.y > 6)
            {
                stat.speedMultiplier = 5f;
            }
            if (transform.position.y - pos.y > 10)
            {
                stat.speedMultiplier = 1000f;
            }
            
            if (noneOfTheAbove)
            {
                stat.speedMultiplier = 1.2f;
            }
            
        }

        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * stat.moveSpeed * stat.speedMultiplier);
        
    }
}
