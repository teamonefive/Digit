using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Defines the XP leveling system
public class Experience : MonoBehaviour
{
    public TileBasedMover tile;
    public Slider levelUpBar;
    public Text currLevel;
    public Text strengthMod;
    public Text agilityMod;
    public Text enduranceMod;
    public Text perceptionMod;
    public Text luckMod;
    public GameObject ui;
    public Button menuButton;
    public Stats stat;
    public Text myGoldDisplay;
    public int MyGold { get; set; }


    private bool isOpen = true;

    private static Experience instance;

    private bool trig4 = true;

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
    // Start is called before the first frame update
    void Start()
    {
        MyGold = 100;
        myGoldDisplay.text = "Gold: " + MyGold.ToString();
        levelUpBar.value = stat.vCurrExp;
        levelUpBar.maxValue = stat.vExpLeft;
        currLevel.text = "Level : 1";
        strengthMod.text = "Strength Modifier : 0";
        agilityMod.text = "Agility Modifier : 0";
        enduranceMod.text = "Endurance Modifier : 0";
        perceptionMod.text = "Perception Modifier : 0";
        luckMod.text = "Luck Modifier : 0";
        Button butn = menuButton.GetComponent<Button>();
        butn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (tile.isDestroyed == true) // bool is set in TileBasedMover after a block is destroyed, line 360
        {
            GainExp(1);
            print("XP Gained"); // prints to the console for debugging
            levelUpBar.value = stat.vCurrExp;
        }
        tile.isDestroyed = false; // reset the bool here NOT in TileBasedMover

        if(Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
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
        levelUpBar.value = stat.vCurrExp;
        stat.vLevel++;
        currLevel.text = "Level : " + stat.vLevel.ToString();
        float t = Mathf.Pow(stat.vExpMod, stat.vLevel);
        stat.vExpLeft = (int)Mathf.Floor(stat.vExpBase * t);
        levelUpBar.maxValue = stat.vExpLeft;
        strengthUp();
        agilityUp();
        enduranceUp();
        perceptionUp();
        luckUp();
        if(trig4 == true)
        {
            trig4 = false;
            FindObjectOfType<DialogueTrigger>().TriggerLvlUpDialogue();
        }
    }
    void strengthUp()
    {
        stat.vStrength++;
        strengthMod.text = "Strength Modifer : " + stat.vStrength.ToString();
        stat.strengthMultiplier *= 0.95f;
    }
    void agilityUp()
    {
        stat.vAgility++;
        agilityMod.text = "Agility Modifier : " + stat.vAgility.ToString();
        stat.climbingDifficulty *= 0.95f;
        //tile.moveSpeed *= 1.5f;
    }
    void enduranceUp()
    {
        stat.vEndurance++;
        enduranceMod.text = "Endurance Modifier : " + stat.vEndurance.ToString();
    }
    void perceptionUp()
    {
        stat.vPerception++;
        perceptionMod.text = "Perception Modifier : " + stat.vPerception.ToString();
    }
    void luckUp()
    {
        stat.vLuck++;
        luckMod.text = "Luck Modifier : " + stat.vLuck.ToString();
    }
    void TaskOnClick() 
    {
        isOpen = !isOpen;
        ui.SetActive(isOpen);
        print(isOpen);
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
}
