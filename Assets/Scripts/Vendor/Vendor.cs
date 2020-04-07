using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField]
    private VendorItem[] items;

    [SerializeField]
    public VendorWindow vendorWindow;

    public void OpenClose()
    {
        vendorWindow.CreatePages(items);
        vendorWindow.Openvwindow();
    }

}
