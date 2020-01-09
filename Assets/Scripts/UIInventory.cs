using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private SlotPanel[] m_slotPanels;

    public void AddItemToUI(Item item) {
        foreach(SlotPanel sp in m_slotPanels ) {
            if ( sp.ContainsEmptySlot() ) {
                sp.AddItem(item);
                break;
            }
        }
    }

    public void RemoveItemFromUI(Item item) {
        foreach(SlotPanel sp in m_slotPanels){
            foreach(UIItem uii in sp.m_uiItems ) {
                if ( uii.m_item == item ) {
                    uii.UpdateItem(null);
                }
            }
        }
    }
}
