using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{
    public enum ItemType { EMPTY, POWERUP, COMPONENT, CONSUMABLE }
    public enum Rarity { COMMON, RARE, EPIC, LEGENDARY }
    public int m_id;
    public string m_name;
    public ItemType m_type;
    public string m_description;
    public Sprite m_sprite;
    public Rarity m_rarity;
    public Dictionary<string, int> m_stats = new Dictionary<string, int>();
    public int m_quantity;
    
    public Item(int id, string name, ItemType type ,Rarity rarity, string description, Dictionary<string,int> stats) {
        m_id = id; m_name = name; m_description = description; m_rarity = rarity; m_type = type; m_stats = stats; m_quantity++;
        m_sprite = Resources.Load<Sprite>("Items/" + m_name);
    }
    public Item(Item item) {
        m_id = item.m_id;
        m_name = item.m_name;
        m_description = item.m_description;
        m_rarity = item.m_rarity;
        m_sprite = item.m_sprite;
        m_stats = item.m_stats;
        m_quantity++;
    }

    
}
