using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIItem : MonoBehaviour, IPointerDownHandler {
    public Item m_item;
    private Image m_sprite;
    private UIItem m_selectedItem;
    private ItemOverview m_itemOverview;
    public bool isCraftingSlot = false;
    private CraftingSlotPanel craftingSlots;

    private void Awake() {
        craftingSlots = FindObjectOfType<CraftingSlotPanel>();
        m_selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        m_sprite = GetComponent<Image>();
        m_itemOverview = FindObjectOfType<ItemOverview>();
        UpdateItem(null);
    }

    public void UpdateItem(Item item) {
        m_item = item;
        if(m_item != null ) {
            m_sprite.color = Color.white;
            m_sprite.sprite = item.m_sprite;
        }
        else {
            m_sprite.color = Color.clear;
        }
        if ( isCraftingSlot ) {
            craftingSlots.UpdateRecipe();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(m_item != null ) {
            m_itemOverview.updateItemOverview(m_item);
            if (m_selectedItem.m_item != null ) {
                Item tmp = new Item(m_selectedItem.m_item);
                m_selectedItem.UpdateItem(this.m_item);
                UpdateItem(tmp);
            }
            else {
                m_selectedItem.UpdateItem(m_item);
                UpdateItem(null);
            }
        }
        else if(m_selectedItem.m_item != null ) {
            UpdateItem(m_selectedItem.m_item);
            m_selectedItem.UpdateItem(null);
        }
    }
}
