using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel_Inventory;
    public GameObject Panel_Bagbar;
    public GameObject GoldDisplayText;

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

    public void OpenGoldDisplayText()
    {
        if (GoldDisplayText != null)
        {
            bool isActive = GoldDisplayText.activeSelf;
            GoldDisplayText.SetActive(!isActive);
        }
    }
}
