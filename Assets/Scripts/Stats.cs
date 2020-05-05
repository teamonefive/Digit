using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public GameObject leaderboard;
    public Button leadboardButton;
    

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

    public bool depthTrig = true;
    public int itemsCrafted = 0;
    public int itemsBought = 0;
    public int itemsSold = 0;
    public int itemsBroken = 0;
    public int totalBags = 1;

    // Dialogue Triggers
    public bool levelUpTrig = true; // In Expereince, levelUp function. Level up dialogue.
    public bool craftTrig = true; // In HideButton, for the crafting at the anvil dialogue
    public bool shopTrig = true; // In HideButton, for the shop dialogue
    public bool fatigueTrig = true; // In Fatigue, wait function. Fatigue dialogue.
    public bool firstPlayTrig = true; // In DialogueTrigger, update. This is the first set of dialogue the player sees when they start the game.
    public bool firstBlockDigTrig = true; // In TutorialCollisionsDigging, using this for the first block the player digs out.
    public bool mineEntranceTrig = true; // In TutorialCollisions, using this to trigger the dialogue when they hit the mine.
    

    //These two might be able to be cut, I used these when I was getting things to work but I found better ways to do triggers

    public bool firstTime = true; // In DialgoueManager, was using this to make sure the start dialogue only triggered once.  Probably redundent now.
    public bool mineTime = true; // In DialogueManager, was using this for the mine entrance and now seems redundent.

    private bool open = false; // open and close for the stats in the UI, private since its only in this script

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

        if (maxDepth == 99 && depthTrig == true)
        {
            FindObjectOfType<DialogueTrigger>().TriggerStatsDialogue();
            depthTrig = false;
        }
        
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
