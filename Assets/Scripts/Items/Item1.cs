using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item1 : ScriptableObject, IMoveable
{
    private static Item1 instance;

    public static Item1 MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Item1>();
            }

            return instance;
        }
    }
    [SerializeField]
    private string description;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    [SerializeField]
    private int price;

    private SlotScript slot;

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public SlotScript MySlot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public Quality MyQuality
    {
        get
        {
            return quality;
        }
    }

    public string MyTitle
    {
        get
        {
            return title;
        }
    }

    public string MyDescription {
        get {
            return description;
        }
    }

    public int MyPrice { get => price; }

    public void Remove()
    {
        if(MySlot!=null)
        {
            MySlot.RemoveItem();
        }
    }
}
