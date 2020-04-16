using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Defines the XP leveling system
public class Experience : MonoBehaviour
{
    public TileBasedMover tile;
    public Slider levelUpBar;

    public Slider enduranceBar;
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
    public GameObject plusPanel;
    public GameObject minusPanel;
    public GameObject Dwarf;
    public Button strP;
    public Button agP;
    public Button endP;
    public Button perP;
    public Button luckP;
    public Button strM;
    public Button agM;
    public Button endM;
    public Button perM;
    public Button luckM;
    public Button confirmation;
    public GameObject confir;
    private int statPoints = 0;
    private int strUp = 0;
    private int agUp = 0;
    private int endUp = 0;
    private int perUp = 0;
    private int lucUp = 0;
    private bool press = false;
    private bool isLvling = false;
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
        enduranceBar.value = 0;
        enduranceBar.maxValue = stat.vFatigue;
        currLevel.text = "1";
        strengthMod.text = "Strength: 0";
        agilityMod.text = "Agility: 0";
        perceptionMod.text = "Perception: 0";
        luckMod.text = "Luck: 0";
        Button butn = menuButton.GetComponent<Button>();
        butn.onClick.AddListener(TaskOnClick);
        Button strp = strP.GetComponent<Button>();
        strp.onClick.AddListener(strengthUp);
        Button agp = agP.GetComponent<Button>();
        agp.onClick.AddListener(agilityUp);
        Button endp = endP.GetComponent<Button>();
        endp.onClick.AddListener(enduranceUp);
        Button perp = perP.GetComponent<Button>();
        perp.onClick.AddListener(perceptionUp);
        Button lucp = luckP.GetComponent<Button>();
        lucp.onClick.AddListener(luckUp);

        Button strm = strM.GetComponent<Button>();
        strm.onClick.AddListener(strengthDown);
        Button agm = agM.GetComponent<Button>();
        agm.onClick.AddListener(agilityDown);
        Button endm = endM.GetComponent<Button>();
        endm.onClick.AddListener(enduranceDown);
        Button perm = perM.GetComponent<Button>();
        perm.onClick.AddListener(perceptionDown);
        Button lucm = luckM.GetComponent<Button>();
        lucm.onClick.AddListener(luckDown);

        Button conf = confirmation.GetComponent<Button>();
        conf.onClick.AddListener(confirm);
    }

    // Update is called once per frame
    void Update()
    {
        if (statPoints > 0)
        {
            StartCoroutine(showMinusStats());
        }
        if(isLvling == true && statPoints != stat.vLevel)
        {
            StopCoroutine("con");
            isLvling = false;
            StartCoroutine(allocatingStats());
        }
        if (tile.isDestroyed == true) // bool is set in TileBasedMover after a block is destroyed, line 360
        {
            GainExp(1);
           // print("XP Gained"); // prints to the console for debugging
            levelUpBar.value = stat.vCurrExp;
        }
        tile.isDestroyed = false; // reset the bool here NOT in TileBasedMover
        enduranceBar.value = enduranceBar.maxValue - stat.vFatigue;

        if (strUp > 0)
        {
            StartCoroutine(showStr());
        }
        if (agUp > 0)
        {
            StartCoroutine(showAg());
        }
        if (endUp > 0)
        {
            StartCoroutine(showEnd());
        }
        if (perUp > 0)
        {
            StartCoroutine(showPer());
        }
        if(lucUp > 0)
        {
            StartCoroutine(showLuck());
        }
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
        currLevel.text = stat.vLevel.ToString();
        float t = Mathf.Pow(stat.vExpMod, stat.vLevel);
        stat.vExpLeft = (int)Mathf.Floor(stat.vExpBase * t);
        levelUpBar.maxValue = stat.vExpLeft;
        Dwarf.GetComponent<TileBasedMover>().enabled = false;
        StartCoroutine(allocatingStats());
        
        if (trig4 == true)
        {
            trig4 = false;
            FindObjectOfType<DialogueTrigger>().TriggerLvlUpDialogue();
        }
    }
    void strengthUp()
    {
        stat.vStrength++;
        strengthMod.text = "Strength: " + stat.vStrength.ToString();
        stat.strengthMultiplier *= 0.95f;
        statPoints++;
        strUp++;
    }
    void agilityUp()
    {
        stat.vAgility++;
        agilityMod.text = "Agility: " + stat.vAgility.ToString();
        stat.climbingDifficulty *= 0.95f;
        statPoints++;
        agUp++;
        //tile.moveSpeed *= 1.5f;
    }
    void enduranceUp()
    {
        stat.vEndurance++;
        enduranceMod.text = "Endurance: " + stat.vEndurance.ToString();
        statPoints++;
        endUp++;
    }
    void perceptionUp()
    {
        stat.vPerception++;
        perceptionMod.text = "Perception: " + stat.vPerception.ToString();
        statPoints++;
        perUp++;
    }
    void luckUp()
    {
        stat.vLuck++;
        luckMod.text = "Luck: " + stat.vLuck.ToString();
        statPoints++;
        lucUp++;
    }
    void strengthDown()
    {
        stat.vStrength--;
        strengthMod.text = "Strength: " + stat.vStrength.ToString();
        stat.strengthMultiplier *= 0.95f;
        statPoints--;
        strUp--;
    }
    void agilityDown()
    {
        stat.vAgility--;
        agilityMod.text = "Agility: " + stat.vAgility.ToString();
        stat.climbingDifficulty *= 0.95f;
        statPoints--;
        agUp--;
        //tile.moveSpeed *= 1.5f;
    }
    void enduranceDown()
    {
        stat.vEndurance--;
        enduranceMod.text = "Endurance: " + stat.vEndurance.ToString();
        statPoints--;
        endUp--;
    }
    void perceptionDown()
    {
        stat.vPerception--;
        perceptionMod.text = "Perception: " + stat.vPerception.ToString();
        statPoints--;
        perUp--;
    }
    void luckDown()
    {
        stat.vLuck--;
        luckMod.text = "Luck: " + stat.vLuck.ToString();
        statPoints--;
        lucUp--;
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
    void confirm()
    {
        press = true;
    }
    IEnumerator allocatingStats()
    {
        confir.SetActive(false);
        plusPanel.SetActive(true);
        yield return new WaitUntil(() => statPoints == stat.vLevel);
        StartCoroutine("con");
    }
    IEnumerator showMinusStats()
    {
        minusPanel.SetActive(true);
        yield return new WaitUntil(() => statPoints == 0);
        minusPanel.SetActive(false);
    }
    IEnumerator showStr()
    {
        strM.GetComponent<Image>().enabled = true;
        yield return new WaitUntil(() => strUp == 0);
        strM.GetComponent<Image>().enabled = false;
    }
    IEnumerator showAg()
    {
        agM.GetComponent<Image>().enabled = true;
        yield return new WaitUntil(() => agUp == 0);
        agM.GetComponent<Image>().enabled = false;
    }
    IEnumerator showEnd()
    {
        endM.GetComponent<Image>().enabled = true;
        yield return new WaitUntil(() => endUp == 0);
        endM.GetComponent<Image>().enabled = false;
    }
    IEnumerator showPer()
    {
        perM.GetComponent<Image>().enabled = true;
        yield return new WaitUntil(() => perUp == 0);
        perM.GetComponent<Image>().enabled = false;
    }
    IEnumerator showLuck()
    {
        luckM.GetComponent<Image>().enabled = true;
        yield return new WaitUntil(() => lucUp == 0);
        luckM.GetComponent<Image>().enabled = false;
    }
    IEnumerator con()
    {
        isLvling = true;
        confir.SetActive(true);
        plusPanel.SetActive(false);
        yield return new WaitUntil(() => press);
        confir.SetActive(false);
        Dwarf.GetComponent<TileBasedMover>().enabled = true;
        statPoints = 0;
        strUp = 0;
        agUp = 0;
        endUp = 0;
        perUp = 0;
        lucUp = 0;
        press = false;
        isLvling = false;
    }
}
