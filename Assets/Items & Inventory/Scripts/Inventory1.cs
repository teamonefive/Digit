using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory1 : MonoBehaviour
{
    [SerializeField] List<Item1> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item1> OnItemRightClickedEvent;

    private void Awake()
    {
        for(int i = 0; i<itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item1 = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item1 = null;
        }
    }

    public bool AddItem(Item1 item)
    {
        if (IsFull())
            return false;

        items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(Item1 item)
    {
        if(items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
