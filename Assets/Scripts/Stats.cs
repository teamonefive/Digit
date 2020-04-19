using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public GameObject leaderboard;
    public Button leadboardButton;
    private bool open = false;

    // FROM EXPERIENCE
    //current level
    public int vLevel = 1;
    //current exp amount
    public int vCurrExp = 0;
    //exp amount needed for lvl 1
    public int vExpBase = 10;
    //exp amount left to next levelup
    public int vExpLeft = 10;
    //modifier that increases needed exp each level
    public float vExpMod = 1.15f;
    //current strength modifier
    public int vStrength = 0;
    //current agility modifer
    public int vAgility = 0;
    //current endurance modifer
    public int vEndurance = 0;
    //current perception modifer
    public int vPerception = 0;
    //current luck modifer
    public int vLuck = 0;

    // FROM TILEBASEDMOVER
    public float moveSpeed = 1f;
    public float climbingDifficulty = 2f;
    public float fallSpeedMultiplier = 1f;
    public float setMoveCooldown = 1f;
    public float moveCooldown = 0f;
    public float tileDifficultyMultiplier = 1f, climbingDifficultyMultiplier = 1f, strengthMultiplier = 1f;

    // FROM FOLLOW DWARF

    public float speedMultiplier = 1f;

    // FROM FATIGUE
    public float vFatigue = 100f;
    public float maxFatigue = 100f;

    // LEADERBOARD STATS
    public int totalIron = 0;
    public int totalSilver = 0;
    public int totalGold = 0;
    public int totalMithril = 0;
    public int totalTopaz = 0;
    public int totalSapphire = 0;
    public int totalRuby = 0;
    public int totalDiamond = 0;

    public int totalDeaths = 0;
    public int totalDigs = 0;
    public int totalMoves = 0;
    public int totalFatigues = 0;

    public int totalPlaytime = 0;
    public int maxDepth = 49;
    public int currentDepth = 49;

    public void statDump()
    {
        Debug.Log("Total Iron: " + totalIron);
        Debug.Log("Total Diamond: " + totalDiamond);
        Debug.Log("Total Moves: " + totalMoves);
        Debug.Log("Total Digs: " + totalDigs);
        Debug.Log("Total Deaths: " + totalDeaths);
        Debug.Log("Total Fatigues: " + totalFatigues);
        Debug.Log("Total Playtime: " + totalPlaytime);
    }

    // Start is called before the first frame update
    void Start()
    {
        Button leadButt = leadboardButton.GetComponent<Button>();
        leadButt.onClick.AddListener(openClose);
    }

    private float period = 0f;

    // Update is called once per frame
    void Update()
    {
        if (period > 1f)
        {
            totalPlaytime++;

            period = 0f;
        }

        period += Time.deltaTime;
        
    }

    void openClose()
    {
        if (open == false)
        {
            leaderboard.SetActive(true);
            open = true;
        }
        else
        {
            leaderboard.SetActive(false);
            open = false;
        }
    }
}
