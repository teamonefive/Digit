using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PickaxeSlot : SlotScript, IPointerClickHandler, IClickable
{
    [SerializeField]
    private Item1 pickaxe;

    public bool IsEmpty
    {
        get
        {
            return pickaxe == null;
        }
    }

    public bool IsFull
    {
        get
        {
            return pickaxe != null;
        }
    }

    public Item1 MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return pickaxe;
            }
            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            if (IsEmpty)
            {
                return 0;
            }
            return 1;
        }
    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    private void Awake()
    {

    }



    public bool AddItem(Item1 item)
    {
        if (pickaxe != null)
        {
            InventoryScript.MyInstance.AddItem(pickaxe);
        }
        pickaxe = item;
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public void RemoveItem()
    {
        pickaxe = null;
    }

    public void Clear()
    {
        pickaxe = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.Fromslot == null && !IsEmpty) //dont have something to move
            {
                if (InventoryScript.MyInstance.AddItem(pickaxe))
                {
                    RemoveItem();
                }

            }
            else if (InventoryScript.MyInstance.Fromslot != null) // have something to move
            {
                AddItem((InventoryScript.MyInstance.Fromslot.MyItems.Pop()));
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (!IsEmpty)
        {
            if (VendorWindow.MyInstance.vwindow.activeSelf)
            {
                Experience.MyInstance.MyGold += (int)(MyItem.MyPrice * 0.8f);
                Experience.MyInstance.myGoldDisplay.text = "Gold: " + Experience.MyInstance.MyGold.ToString();
                RemoveItem();
            }
        }
    }
}
