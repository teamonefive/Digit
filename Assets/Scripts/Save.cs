using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save : MonoBehaviour
{
    public GameObject dwarf;
    public GameObject lamp;
    public MapGen world;
    public Stats stat;
    public Gold gold;
    public Bag bag;
    public SlotScript pickaxeSlot;
    public Item1[] allItems;

    private void Awake()
    {
        world = GameObject.FindObjectOfType<MapGen>();
        gold = GameObject.FindObjectOfType<Gold>();
    }
    public void createSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "dygg.sav");
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData save = new SaveData();

        formatter.Serialize(stream, save);
        stream.Close();

    }

    public void loadSave()
    {
        string path = Path.Combine(Application.persistentDataPath, "dygg.sav");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData save = formatter.Deserialize(stream) as SaveData;

            stream.Close();

            //restore game state here
            world.UnrenderAllTiles();

            dwarf.transform.position = new Vector3(save.dwarfPosX, save.dwarfPosY, save.dwarfPosZ);

            lamp.transform.position = new Vector3(save.lampPosX, save.lampPosY, save.lampPosZ);

            world.seed = save.mapSeed;
            world.mountainHeights = save.mountainHeights;

            world.destroyedTiles = new Dictionary<Vector2, bool>();
            foreach (mapElement elem in save.destroyedTiles)
            {
                world.destroyedTiles.Add(new Vector2(elem.x, elem.y), elem.b);
            }

            world.isWaterNotLava = new Dictionary<Vector2, bool>();
            foreach (mapElement elem in save.isWaterNotLava)
            {
                world.isWaterNotLava.Add(new Vector2(elem.x, elem.y), elem.b);
            }

            world.generatedStone = new Dictionary<Vector2, bool>();
            foreach (mapElement elem in save.generatedStone)
            {
                world.generatedStone.Add(new Vector2(elem.x, elem.y), elem.b);
            }

            world.generateStartingTiles(false);

            stat.vLevel = save.vLevel;
            stat.vCurrExp = save.vCurrExp;
            stat.vExpBase = save.vExpBase;
            stat.vExpLeft = save.vExpLeft;
            stat.vStrength = save.vStrength;
            stat.vAgility = save.vAgility;
            stat.vEndurance = save.vEndurance;
            stat.vPerception = save.vPerception;
            stat.vLuck = save.vLuck;
            stat.strengthMultiplier = save.strengthMultiplier;
            stat.climbingDifficulty = save.climbingDifficulty;
            stat.vFatigue = save.vFatigue;
            stat.maxFatigue = save.maxFatigue;
            stat.totalIron = save.totalIron;
            stat.totalSilver = save.totalSilver;
            stat.totalGold = save.totalGold;
            stat.totalMithril = save.totalMithril;
            stat.totalTopaz = save.totalTopaz;
            stat.totalSapphire = save.totalSapphire;
            stat.totalRuby = save.totalRuby;
            stat.totalDiamond = save.totalDiamond;
            stat.totalDeaths = save.totalDeaths;
            stat.totalDigs = save.totalDigs;
            stat.totalMoves = save.totalMoves;
            stat.totalFatigues = save.totalFatigues;
            stat.totalPlaytime = save.totalPlaytime;
            stat.maxDepth = save.maxDepth;
            stat.currentDepth = save.currentDepth;
            stat.itemsCrafted = save.itemsCrafted;
            stat.itemsBought = save.itemsBought;
            stat.itemsSold = save.itemsSold;
            stat.itemsBroken = save.itemsBroken;
            stat.depthTrig = save.depthTrig;

            gold.gold = save.gold;
            stat.totalBags = save.bags;
            for (int i = 1; i < stat.totalBags; i++)
            {
                Item1 item = Instantiate(bag);
                InventoryScript.MyInstance.AddItem(item);
                ((Bag)item).Use();
            }

            pickaxeSlot.Clear();
            Item1 pickaxeItem = generateItem(save.pickaxe);
            if (pickaxeItem != null)
            {
                pickaxeItem = Instantiate(pickaxeItem);
                pickaxeItem.myDurability = save.pickaxe.durability;
                pickaxeSlot.AddItem(pickaxeItem);
            }


            SlotScript[] slots = GameObject.FindObjectsOfType<SlotScript>();
            foreach (SlotScript slot in slots)
            {
                if (slot != pickaxeSlot)
                {
                    slot.Clear();
                }
            }

            foreach (itemElement elem in save.items)
            {
                Item1 item = generateItem(elem);
                if (item != null)
                {
                    item = Instantiate(item);
                    item.myDurability = elem.durability;
                    InventoryScript.MyInstance.AddItem(item);
                }
            }
        }

        else
        {
            Debug.Log("save file not found. Save should be located at " + path);
        }
    }

    public Item1 generateItem(itemElement elem)
    {
        if (elem.title == "")
        {
            return null;
        }
        else if (elem.title == "Iron Ore")
        {
            return allItems[0];
        }
        else if (elem.title == "Silver Ore")
        {
            return allItems[1];
        }
        else if (elem.title == "Gold Ore")
        {
            return allItems[2];
        }
        else if (elem.title == "Mithril Ore")
        {
            return allItems[3];
        }
        else if (elem.title == "Iron Ingot")
        {
            return allItems[4];
        }
        else if (elem.title == "Silver Ingot")
        {
            return allItems[5];
        }
        else if (elem.title == "Gold Ingot")
        {
            return allItems[6];
        }
        else if (elem.title == "Mithril Ingot")
        {
            return allItems[7];
        }
        else if (elem.title == "Topaz Gem")
        {
            return allItems[8];
        }
        else if (elem.title == "Sapphire Gem")
        {
            return allItems[9];
        }
        else if (elem.title == "Ruby Gem")
        {
            return allItems[10];
        }
        else if (elem.title == "Diamond Gem")
        {
            return allItems[11];
        }
        else if (elem.title == "Wood Pickaxe")
        {
            return allItems[12];
        }
        else if (elem.title == "Iron Pickaxe")
        {
            return allItems[13];
        }
        else if (elem.title == "Silver Pickaxe")
        {
            return allItems[14];
        }
        else if (elem.title == "Gold Pickaxe")
        {
            return allItems[15];
        }
        else if (elem.title == "Mithril Pickaxe")
        {
            return allItems[16];
        }
        else if (elem.title == "Box of Rations")
        {
            return allItems[17];
        }
        else if (elem.title == "Rune of Teleportation")
        {
            return allItems[18];
        }

        return null;

    }
}

[System.Serializable]
public class SaveData
{
    public float dwarfPosX, dwarfPosY, dwarfPosZ;
    public float lampPosX, lampPosY, lampPosZ;
    public int mapSeed;
    public Dictionary<int, int> mountainHeights;
    public mapElement[] destroyedTiles;
    public mapElement[] isWaterNotLava;
    public mapElement[] generatedStone;
    public int vLevel, vCurrExp, vExpBase, vExpLeft, vStrength, vAgility, vEndurance, vPerception, vLuck;
    public float strengthMultiplier, climbingDifficulty, vFatigue, maxFatigue;
    public int totalIron, totalSilver, totalGold, totalMithril, totalTopaz, totalSapphire, totalRuby, totalDiamond, totalDeaths, totalDigs, totalMoves, totalFatigues, totalPlaytime, maxDepth, currentDepth, itemsCrafted, itemsBought, itemsSold, itemsBroken;
    public bool depthTrig;
    public int gold, bags;
    public itemElement pickaxe;
    public List<itemElement> items;

    public SaveData()
    {
        Save save = GameObject.FindObjectOfType<Save>();

        dwarfPosX = save.dwarf.transform.position.x;
        dwarfPosY = save.dwarf.transform.position.y;
        dwarfPosZ = save.dwarf.transform.position.z;

        lampPosX = save.lamp.transform.position.x;
        lampPosY = save.lamp.transform.position.y;
        lampPosZ = save.lamp.transform.position.z;

        mapSeed = save.world.seed;
        mountainHeights = save.world.mountainHeights;

        int i = 0;
        destroyedTiles = new mapElement[save.world.destroyedTiles.Count];
        foreach(KeyValuePair<Vector2, bool> elem in save.world.destroyedTiles)
        {
            destroyedTiles[i++] = new mapElement(elem.Key.x, elem.Key.y, elem.Value);
        }

        i = 0;
        isWaterNotLava = new mapElement[save.world.isWaterNotLava.Count];
        foreach (KeyValuePair<Vector2, bool> elem in save.world.isWaterNotLava)
        {
            isWaterNotLava[i++] = new mapElement(elem.Key.x, elem.Key.y, elem.Value);
        }

        i = 0;
        generatedStone = new mapElement[save.world.generatedStone.Count];
        foreach (KeyValuePair<Vector2, bool> elem in save.world.isWaterNotLava)
        {
            generatedStone[i++] = new mapElement(elem.Key.x, elem.Key.y, elem.Value);
        }

        vLevel = save.stat.vLevel;
        vCurrExp = save.stat.vCurrExp;
        vExpBase = save.stat.vExpBase;
        vExpLeft = save.stat.vExpLeft;
        vStrength = save.stat.vStrength;
        vAgility = save.stat.vAgility;
        vEndurance = save.stat.vEndurance;
        vPerception = save.stat.vPerception;
        vLuck = save.stat.vLuck;
        strengthMultiplier = save.stat.strengthMultiplier;
        climbingDifficulty = save.stat.climbingDifficulty;
        vFatigue = save.stat.vFatigue;
        maxFatigue = save.stat.maxFatigue;
        totalIron = save.stat.totalIron;
        totalSilver = save.stat.totalSilver;
        totalGold = save.stat.totalGold;
        totalMithril = save.stat.totalMithril;
        totalTopaz = save.stat.totalTopaz;
        totalSapphire = save.stat.totalSapphire;
        totalRuby = save.stat.totalRuby;
        totalDiamond = save.stat.totalDiamond;
        totalDeaths = save.stat.totalDeaths;
        totalDigs = save.stat.totalDigs;
        totalMoves = save.stat.totalMoves;
        totalFatigues = save.stat.totalFatigues;
        totalPlaytime = save.stat.totalPlaytime;
        maxDepth = save.stat.maxDepth;
        currentDepth = save.stat.currentDepth;
        itemsCrafted = save.stat.itemsCrafted;
        itemsBought = save.stat.itemsBought;
        itemsSold = save.stat.itemsSold;
        itemsBroken = save.stat.itemsBroken;
        depthTrig = save.stat.depthTrig;

        gold = save.gold.gold;
        bags = save.stat.totalBags;

        Item1 item = save.pickaxeSlot.MyItem;
        pickaxe = new itemElement("");
        if (item != null)
        {
            pickaxe.title = item.MyTitle;
            pickaxe.durability = item.myDurability;
        }

        items = new List<itemElement>();

        SlotScript[] slots = GameObject.FindObjectsOfType<SlotScript>();
        foreach(SlotScript slot in slots)
        {
            if (slot != save.pickaxeSlot)
            {
                if (slot.MyItem != null)
                {
                    itemElement elem = new itemElement(slot.MyItem.MyTitle, slot.MyItem.myDurability);
                    for (i = 0; i < slot.MyCount; i++)
                    { 
                        items.Add(elem); 
                    }
                }
            }
        }

}
}

[System.Serializable]
public class mapElement
{
    public float x, y;
    public bool b;
    public mapElement(float x, float y, bool b)
    {
        this.x = x;
        this.y = y;
        this.b = b;
    }
}

[System.Serializable] 
public class itemElement
{
    public string title;
    public int durability;
    public itemElement(string title, int durability = 0)
    {
        this.title = title;
        this.durability = durability;
    }
}