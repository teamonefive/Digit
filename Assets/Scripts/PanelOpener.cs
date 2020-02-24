using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel_Inventory;
    public GameObject Panel_Bagbar;
    //public GameObject Panel3;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }

    public void OpenPanel1()
    {
        if (Panel_Inventory != null)
        {
            bool isActive = Panel_Inventory.activeSelf;
            Panel_Inventory.SetActive(!isActive);
        }
    }

    public void OpenPanel2()
    {
        if (Panel_Bagbar != null)
        {
            bool isActive = Panel_Bagbar.activeSelf;
            Panel_Bagbar.SetActive(!isActive);
        }
    }

    //public void OpenPanel3()
    //{
    //    if (Panel3 != null)
    //    {
    //        bool isActive = Panel3.activeSelf;
    //        Panel3.SetActive(!isActive);
    //    }
    //}
}
