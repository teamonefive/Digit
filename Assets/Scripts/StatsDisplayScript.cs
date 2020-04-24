using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayScript : MonoBehaviour
{
    public Stats stat;

    public Text iron;
    public Text diamond;
    public Text silver;
    public Text gold;
    public Text topaz;
    public Text sapphire;
    public Text ruby;
    public Text mithril;
    public Text moves;
    public Text digs;
    public Text deaths;
    public Text fatigues;
    public Text playTime;
    public Text maxDepth;
    public Text crafted;
    public Text bought;
    public Text sold;
    public Text broken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iron.text = "Total Iron: " + stat.totalIron;
        silver.text = "Total Silver: " + stat.totalSilver;
        gold.text = "Total Gold: " + stat.totalGold;
        mithril.text = "Total Mithril: " + stat.totalMithril;
        topaz.text = "Total Topaz: " + stat.totalTopaz;
        sapphire.text = "Total Sapphire: " + stat.totalSapphire;
        ruby.text = "Total Ruby: " + stat.totalRuby;
        diamond.text = "Total Diamond: " + stat.totalDiamond;
        moves.text = "Total Moves: " + stat.totalMoves;
        digs.text = "Total Digs: " + stat.totalDigs;
        deaths.text = "Total Deaths: " + stat.totalDeaths;
        fatigues.text = "Total Fatigues: " + stat.totalFatigues;
        playTime.text = "Total Playtime: " + stat.totalPlaytime;
        maxDepth.text = "Max Depth Reached: " + (stat.maxDepth - 49);
        crafted.text = "Total Items Crafted: " + stat.itemsCrafted;
        bought.text = "Total Items Purchased: " + stat.itemsBought;
        sold.text = "Total Items Sold: " + stat.itemsSold;
        broken.text = "Total Equipment Broken: " + stat.itemsBroken;
}
}
