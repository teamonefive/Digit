﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SampleButton : MonoBehaviour
{
    public Button button;
    public Text nameLabel;
    public Text priceLabel;
    public Image iconImage;

    private Itemtemp item;
    private ShopScrollList scrollList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(Itemtemp currentItem, ShopScrollList currentScrollList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        priceLabel.text = item.price.ToString();
        iconImage.sprite = item.icon;

        scrollList = currentScrollList;
    }


}
