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

    public Item1 Item { get => item;}
    public int Quantity { get => quantity; set => quantity = value; }
    public bool Unlimited { get => unlimited; }
}
