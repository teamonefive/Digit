using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOverview : MonoBehaviour{
    public ItemDatabase itemDatabase;
    public Item m_item;
    public Text m_name;
    public Text m_description;
    public Text m_stats;
    public Image m_image;

    private void Awake() {
        itemDatabase = FindObjectOfType<ItemDatabase>();
    }
    private void Start() {
        m_item = itemDatabase.GetItem(0);
        m_name.text = m_item.m_name;
        m_description.text = m_item.m_description;
        m_image.sprite = Resources.Load<Sprite>("Items/" + m_item.m_name);
        m_stats.text = "";
        switch ( m_item.m_rarity ) {
            case Item.Rarity.COMMON:
                m_name.color = Color.white;
                break;
            case Item.Rarity.RARE:
                m_name.color = Color.blue;
                break;
            case Item.Rarity.EPIC:
                m_name.color = Color.magenta;
                break;
            case Item.Rarity.LEGENDARY:
                m_name.color = Color.yellow;
                break;
        }
    }

    public void updateItemOverview(Item item) {
        Debug.Log("updateItemOverview called with item " + item.m_id);
        m_item = item;
        m_name.text = m_item.m_name;
        m_description.text = m_item.m_description;
        m_image.sprite = Resources.Load<Sprite>("Items/" + m_item.m_name);
        m_stats.text = formatStats(item);
        switch ( m_item.m_rarity ) {
            case Item.Rarity.COMMON:
                m_name.color = Color.white;
                break;
            case Item.Rarity.RARE:
                m_name.color = Color.blue;
                break;
            case Item.Rarity.EPIC:
                m_name.color = Color.magenta;
                break;
            case Item.Rarity.LEGENDARY:
                m_name.color = Color.yellow;
                break;
        }
    }

    private string formatStats(Item item) {
        string val = "";
        foreach(KeyValuePair<string, int> entry in item.m_stats ) {
            val += "<b>" + entry.Key + "</b>: " + entry.Value + "\n";
        }
        return val;
    }
}
