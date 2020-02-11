using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VendorItem 
{
    [SerializeField]
    private Item1 item;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private bool unlimited;

    public Item1 MyItem { get => item;}
    public int MyQuantity { get => quantity; set => quantity = value; }
    public bool Unlimited { get => unlimited; }
}
