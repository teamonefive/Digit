using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item1 : ScriptableObject, IMoveable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

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

    public void Remove()
    {
        if(MySlot!=null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
