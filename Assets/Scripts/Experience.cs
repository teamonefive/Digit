using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Defines the XP leveling system
public class Experience : MonoBehaviour
{
    public TileBasedMover tile;

    public Stats stat;

    public GameObject Dwarf;

    public int statPoints = 0;
    public int strUp = 0;
    public int agUp = 0;
    public int endUp = 0;
    public int perUp = 0;
    public int lucUp = 0;

    public bool isLvling = false;

    private static Experience instance;

    public static Experience MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Experience>();
            }

            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // bool is set in TileBasedMover after a block is destroyed, line 360
        if (tile.isDestroyed == true) 
        {
            GainExp(1);
        }
        // reset the bool here NOT in TileBasedMover
        tile.isDestroyed = false; 
    }

    public void GainExp(int e)
    {
        stat.vCurrExp += e;
        if (stat.vCurrExp >= stat.vExpLeft)
        {
            LvlUp();
        }
    }
    void LvlUp()
    {
        stat.vCurrExp -= stat.vExpLeft;
        stat.vLevel++;
        float t = Mathf.Pow(stat.vExpMod, stat.vLevel);
        stat.vExpLeft = (int)Mathf.Floor(stat.vExpBase * t);
        Dwarf.GetComponent<TileBasedMover>().enabled = false;
        isLvling = true;
        
        if (stat.levelUpTrig == true)
        {
            stat.levelUpTrig = false;
            FindObjectOfType<DialogueTrigger>().TriggerLvlUpDialogue();
        }
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if(clickable.MyCount>1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }

        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }
    public void confirm()
    {
        Dwarf.GetComponent<TileBasedMover>().enabled = true;
        isLvling = false;
        statPoints = 0;

        for (int i = 0; i < strUp; i++)
        {
            stat.strengthMultiplier *= 0.95f;
        }
        for (int i = 0; i < agUp; i++)
        {
            stat.climbingDifficulty *= 0.95f;
        }
        for (int i = 0; i < endUp; i++)
        {
            stat.maxFatigue += 25f;
        }
        stat.vFatigue = stat.maxFatigue;

        stat.vStrength += strUp;
        stat.vAgility += agUp;
        stat.vEndurance += endUp;
        stat.vPerception += perUp;
        stat.vLuck += lucUp;

        strUp = 0;
        agUp = 0;
        endUp = 0;
        perUp = 0;
        lucUp = 0;
    }
}
