using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorWindow : MonoBehaviour
{
    public GameObject vwindow;

    [SerializeField]
    private VendorButton[] vendorButtons;

    public void CreatePages(VendorItem[] items)
    {
        for (int i =0; i<items.Length; i++)
        {
            vendorButtons[i].AddItem(items[i]);
        }
    }

    public void Openvwindow()
    {
        if (vwindow != null)
        {
            bool isActive = vwindow.activeSelf;

            vwindow.SetActive(!isActive);
        }
    }
}
