using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPanel : MonoBehaviour {
    public List<UIItem> m_uiItems = new List<UIItem>();
    public int m_numberOfSlots;
    public GameObject m_slotPrefab;

    private void Awake() {
        for(int i = 0; i < m_numberOfSlots; i++ ) {
            GameObject instance = Instantiate(m_slotPrefab);
            instance.transform.SetParent(transform);
            m_uiItems.Add(instance.GetComponentInChildren<UIItem>());
            m_uiItems[i].m_item = null;
        }
    }
    public void UpdateSlot(int slot, Item item) {
        m_uiItems[slot].UpdateItem(item);
    }

    public void AddItem(Item item) {
        UpdateSlot(m_uiItems.FindIndex(index => index.m_item == null),item);
    }
    public void RemoveItem(Item item) {
        UpdateSlot(m_uiItems.FindIndex(index => index.m_item == item), null);
    }

    public bool ContainsEmptySlot() {
        foreach(UIItem uii in m_uiItems ) {
            if(uii.m_item == null ) {
                return true;
            }
        }
        return false;
    }
}
