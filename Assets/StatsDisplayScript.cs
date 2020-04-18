using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayScript : MonoBehaviour
{
    public Stats stat;

    public Text iron;
    public Text diamond;
    public Text moves;
    public Text digs;
    public Text deaths;
    public Text fatigues;
    public Text playTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iron.text = "Total Iron: " + stat.totalIron;
        diamond.text = "Total Diamond: " + stat.totalDiamond;
        moves.text = "Total Moves: " + stat.totalMoves;
        digs.text = "Total Digs: " + stat.totalDigs;
        deaths.text = "Total Deaths: " + stat.totalDeaths;
        fatigues.text = "Total Fatigues: " + stat.totalFatigues;
        playTime.text = "Total Playtime: " + stat.totalPlaytime;
    }
}
