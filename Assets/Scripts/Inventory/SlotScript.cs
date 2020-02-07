using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable
{
    private Stack<Item1> items = new Stack<Item1>();

    [SerializeField]
    private Image icon;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    public Item1 MyItem
    {
        get
        {
            if(!IsEmpty)
            {
                return items.Peek();
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
            return items.Count;
        }
    }

    public bool AddItem(Item1 item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public void RemoveItem(Item1 item)
    {
        if(!IsEmpty)
        {
            items.Pop();
            Experience.MyInstance.UpdateStackSize(this);
        } 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if(MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
    }
}
