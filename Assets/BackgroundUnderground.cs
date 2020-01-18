//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BackgroundUnderground : MonoBehaviour
//{
//    private float length, startPosX;
//    public GameObject camera;
//    public float xParallax;

//    // Start is called before the first frame update
//    void Start()
//    {
//        startPosX = transform.position.x;
//        length = GetComponent<SpriteRenderer>().bounds.size.x;

//    }

//    // Update is called once per frame
//    void Update()
//    {

//        float xDist = (camera.transform.position.x * xParallax);

//        transform.position = new Vector3(startPosX + xDist, transform.position.y, transform.position.z);

//        if (camera.transform.position.y >= 0)
//        {
//            GetComponent<SpriteRenderer>().sortingOrder = -3;
//        }
//        else
//        {
//            GetComponent<SpriteRenderer>().sortingOrder = -2;
//        }

//    }
//}
