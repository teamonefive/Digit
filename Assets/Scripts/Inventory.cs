using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MAXINVENTORYSIZE = 20;
    public int m_size;
    public List<Item> m_playerItems = new List<Item>();
    ItemDatabase m_itemDatabase;
    ItemOverview m_itemOverview;

    [SerializeField]
    private UIInventory m_UIInventory;

    private void Awake() {
        m_itemDatabase = FindObjectOfType<ItemDatabase>();
        m_itemOverview = FindObjectOfType<ItemOverview>();
    }

    private void Start() {
        AddItem(1);
        AddItem(2);
        AddItem(3);
    }



    public void AddItem(int id) {
        foreach ( Item i in m_playerItems ) {
            if ( i.m_id == id ) {
                i.m_quantity++;
                return;
            }
        }
        if (m_size < MAXINVENTORYSIZE ) {
            Item tmp = m_itemDatabase.GetItem(id);
            m_UIInventory.AddItemToUI(tmp);
            m_playerItems.Add(tmp);
            m_size++;
        }

    }

    public void RemoveItem(int id) {
        foreach(Item i in m_playerItems ) {
            if ( i.m_id == id ) {
                if(i.m_quantity > 1 ) {
                    i.m_quantity--;
                    return;
                }
                else if(i.m_quantity == 1 ) {
                    m_playerItems.Remove(i);
                    i.m_quantity--;
                    m_UIInventory.RemoveItemFromUI(i);
                    return;
                }
                else {
                    Debug.LogError("item being removed has a negative quantity");
                    return;
                }

            }
        }
    }

    public Item CheckForItem(int id) {
        return m_playerItems.Find(item => item.m_id == id);
    }
}
