using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public GameObject LootWindow;

    public void OpenLootWindow()
    {
        if(LootWindow != null)
        {
            bool isActive = LootWindow.activeSelf;

            LootWindow.SetActive(!isActive);
        }
    }
}
