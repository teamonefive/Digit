using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Itemtemp
{
    public string itemName;
    public Sprite icon;
    public float price = 1f;
}



public class ShopScrollList : MonoBehaviour
{
    public List<Itemtemp> itemList;
    public Transform contentPanel;
    public ShopScrollList otherShop;
    public Text myGoldDisplay;
    public SimpleObjectPool buttonObjectPool;
    public float gold = 20f;

    private void Start()
    {
        
    }

    private void AddButtons()
    {
        for (int i = 0; i<itemList.Count; i++)
        {
            Itemtemp item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
        }
    }
}
