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
    public GameObject ui;
    public Button menuButton;
    public Fatigue fat;

    private bool isOpen = true;

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

    // Start is called before the first frame update
    void Start()
    {
        levelUpBar.value = vCurrExp;
        levelUpBar.maxValue = vExpLeft;
        currLevel.text = "Level : 1";
        strengthMod.text = "Strength Modifier : 0";
        agilityMod.text = "Agility Modifier : 0";
        Button butn = menuButton.GetComponent<Button>();
        butn.onClick.AddListener(TaskOnClick);

    }

    // Update is called once per frame
    void Update()
    {
        if (tile.isDestroyed == true) // bool is set in TileBasedMover after a block is destroyed, line 410
        {
            GainExp(1);
            print("XP Gained"); // prints to the console for debugging
            levelUpBar.value = vCurrExp;
        }
        tile.isDestroyed = false; // reset the bool here NOT in TileBasedMover
    }

    public void GainExp(int e)
    {
        vCurrExp += e;
        if (vCurrExp >= vExpLeft)
        {
            LvlUp();
        }
    }
    void LvlUp()
    {
        vCurrExp -= vExpLeft;
        levelUpBar.value = vCurrExp;
        vLevel++;
        currLevel.text = "Level : " + vLevel.ToString();
        float t = Mathf.Pow(vExpMod, vLevel);
        vExpLeft = (int)Mathf.Floor(vExpBase * t);
        levelUpBar.maxValue = vExpLeft;
        strengthUp();
        agilityUp();
    }
    void strengthUp()
    {
        vStrength++;
        strengthMod.text = "Strength Modifer : " + vStrength.ToString();
        tile.strengthMultiplier *= 0.95f;
    }
    void agilityUp()
    {
        vAgility++;
        agilityMod.text = "Agility Modifier : " + vAgility.ToString();
        tile.climbingDifficulty *= 0.95f;
        //tile.moveSpeed *= 1.5f;
    }
    void TaskOnClick() 
    {
        isOpen = !isOpen;
        ui.SetActive(isOpen);
    }
}
