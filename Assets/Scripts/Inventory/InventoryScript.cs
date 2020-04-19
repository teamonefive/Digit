using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item1 item);
public class InventoryScript : MonoBehaviour
{
    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    private SlotScript fromSlot;
    public event ItemCountChanged itemCountChangedEvent;
    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    //DEBUGGING
    [SerializeField]
    public Item1[] items;

    public bool CanAddBag
    {
        get { return bags.Count < 4; }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }
            return count;
        }
    }

    public SlotScript Fromslot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;
            if(value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }


    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(16);
        bag.Use();
    }

    private void Update()
    {

    }

    public void AddBag(Bag bag)
    {
        foreach(BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                bag.MyBagButton = bagButton;
                break;
            }
        }
    }

    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public bool AddItem(Item1 item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }

        return PlaceInEmpty(item);
    }

    private bool PlaceInEmpty(Item1 item)
    {
        foreach (Bag bag in bags)//Checks all bags
        {
            if (bag.MyBagScript.AddItem(item)) //Tries to add the item
            {
                OnItemCountChanged(item);
                return true; //It was possible to add the item
            }
        }

        return false;
    }

    
    private bool PlaceInStack(Item1 item)
    {
        foreach (Bag bag in bags)
        {
            foreach(SlotScript slots in bag.MyBagScript.MySlots)
            {
                if(slots.StackItem(item))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        foreach(Bag bag in bags)
        {
            if(bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }
    public void OnItemCountChanged(Item1 item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }

    public bool IsCraftable(Item1 itemName, int quantity) {
        foreach(Bag bag in bags ) {
            foreach(SlotScript slots in bag.MyBagScript.MySlots ) {
                if(slots.MyItems.Count == 0 ) {
                    return false;
                }
                else if( slots.MyItems.Peek().MyTitle == itemName.MyTitle && slots.MyItems.Count == quantity ) {
                    return true;
                }
            }
        }
        return false;
    }
    public bool Craft(Item1 itemName, int quantity) {
        foreach ( Bag bag in bags ) {
            foreach ( SlotScript slots in bag.MyBagScript.MySlots ) {
                if ( slots.MyItems.Peek().MyTitle == itemName.MyTitle && slots.MyItems.Count == quantity ) {
                    Debug.Log("Item and count found");
                    Debug.Log("Removing Item");
                    for(int i = 0; i < quantity; i++ ) {
                        slots.MyItems.Pop();
                    }
                    return true;
                }
            }
        }
        Debug.Log("Item not found");
        return false;
    }
}


