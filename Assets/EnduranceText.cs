using UnityEngine;
using UnityEngine.UI;

public class EnduranceText : MonoBehaviour
{
    private Text text;
    public Stats stats;
    private string endurance;
    private string current;
    private string leveling;

    void Start()
    {
        text = GetComponent<Text>();
        endurance = "Endurance: ";
        current = stats.vEndurance.ToString("0");
        text.text = endurance + stats.vEndurance.ToString("0");
    }

    void Update()
    {
        current = stats.vEndurance.ToString("0");
        if (Experience.MyInstance.isLvling)
        {
            leveling = Experience.MyInstance.endUp.ToString("0");
            if (Experience.MyInstance.endUp > 0)
            {
                text.text = endurance + current + " +" + leveling;
            }
            else
            {
                text.text = endurance + current;
            }
        }
        else
        {
            // Just show the text.
            text.text = endurance + current;
        }
    }
}
