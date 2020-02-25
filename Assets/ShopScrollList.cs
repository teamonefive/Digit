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

    void Start()
    {
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        myGoldDisplay.text = "fuck: " + gold.ToString();
        RemoveButtons();
        AddButtons();
    }

    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Itemtemp item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel,false);


            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }

    private void RemoveButtons()
    {
      while(contentPanel.childCount>0)
      {
        GameObject toRemove = transform.GetChild(0).gameObject;
        buttonObjectPool.ReturnObject(toRemove);
      }
    }

    public void TryTransferItemToOtherShop (Itemtemp item)
    {
      if(otherShop.gold>= item.price)
      {
        gold += item.price;
        otherShop.gold -= item.price;
        AddItem(item, otherShop);
        RemoveItem(item,this);

        RefreshDisplay();
        otherShop.RefreshDisplay();
      }
    }

    private void AddItem(Itemtemp itemToAdd, ShopScrollList shopList)
    {
      shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Itemtemp itemToRemove, ShopScrollList shopList)
    {
      for(int i = shopList.itemList.Count -1; i>=0 ; i--)
      {
        if(shopList.itemList[i] == itemToRemove)
        {
          shopList.itemList.RemoveAt(i);
        }
      }
    }
}
