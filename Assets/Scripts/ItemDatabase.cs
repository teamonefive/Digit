using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    public List<Item> m_items = new List<Item>();

    private void Awake() {
        BuildItemDatabase();
    }


    //Looks through the database and returns an item if it finds one that matches the input id else returns null
    public Item GetItem(int id) {
        return m_items.Find(item => item.m_id == id);
    }


    void BuildItemDatabase() {
        m_items = new List<Item>() {
            new Item(0,"NULL",Item.ItemType.EMPTY,Item.Rarity.COMMON , "NULL",
            new Dictionary<string, int>{
                { "NULL", 0 }
            }),
            new Item(1,"Diamond Pickaxe",Item.ItemType.POWERUP, Item.Rarity.EPIC, "Congratulations, you have reached the pinacle of antiquated mining technology!",
            new Dictionary<string, int>{
                { "BLOCKDAMAGE", 100 },
                { "SPEED", 2 },
                { "VALUE", 1000 }
            }),
            new Item(2,"Diamond Ore",Item.ItemType.COMPONENT, Item.Rarity.EPIC, "a chunk of raw diamond",
            new Dictionary<string, int>{
                { "VALUE", 400 }
            }),
            new Item(3,"Silver Pickaxe",Item.ItemType.COMPONENT, Item.Rarity.RARE, "a pickaxe made of steel",
            new Dictionary<string, int>{
                { "BLOCKDAMAGE", 50 },
                { "SPEED", 2 },
                { "VALUE", 100 }
            })
        };
    }
}
