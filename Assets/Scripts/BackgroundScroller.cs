using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    private float length, height, startPosX, startPosY;
    public GameObject camera;
    public float xParallax, yParallax;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    // Update is called once per frame
    void Update()
    {
        float temp = (camera.transform.position.x * (1 - xParallax));

        float xDist = (camera.transform.position.x * xParallax);
        float yDist = (camera.transform.position.y * yParallax);

        transform.position = new Vector3(startPosX + xDist, startPosY + yDist, transform.position.z);

        if (temp > startPosX + length)
        {
            startPosX += length;
        }
        else if (temp < startPosX - length)
        {
            startPosX -= length;
        }
    }
}
