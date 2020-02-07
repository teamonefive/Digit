using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
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

    public bool AddItem(Item1 item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        return true;
    }
}
