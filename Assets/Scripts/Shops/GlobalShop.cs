using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalShop : MonoBehaviour
{
    public static string Item01;
    public static string Item02;
    public static string Item03;
    public static string Item04;
    public static int ShopNumber;


    void Update()
    {
        if (ShopNumber == 1)
        {
            Item01 = "blue portion";
            Item02 = "yellow portion";
            Item03 = "red portion";
            Item04 = "green portion";
        }
        if (ShopNumber == 2)
        {
            Item01 = "purple portion";
            Item02 = "black portion";
            Item03 = "white portion";
            Item04 = "orange portion";
        }
    }
}
