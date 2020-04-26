using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public Stats stats;
    private Text text;
    public int gold;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        gold = 100;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Gold: " + gold.ToString("0");
    }

    public void addGold(int g)
    {
        gold += g;
    }

    public void removeGold(int g)
    {
        gold -= g;
    }

    public int getGold()
    {
        return gold;
    }
}
