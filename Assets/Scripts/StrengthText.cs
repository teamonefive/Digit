using UnityEngine;
using UnityEngine.UI;

public class StrengthText : MonoBehaviour
{
    private Text text;
    public Stats stats;
    private string strength;
    private string current;
    private string leveling;

    void Start()
    {
        text = GetComponent<Text>();
        strength = "Strength: ";
        current = stats.vStrength.ToString("0");
        text.text = strength + stats.vStrength.ToString("0");
    }

    void Update()
    {
        current = stats.vStrength.ToString("0");
        if (Experience.MyInstance.isLvling)
        {
            leveling = Experience.MyInstance.strUp.ToString("0");
            if (Experience.MyInstance.strUp > 0)
            {
                text.text = strength + current + " +" + leveling;
            } 
            else
            {
                text.text = strength + current;
            }
        } 
        else
        {
            // Just show the text.
            text.text = strength + current;
        } 
    }
}
