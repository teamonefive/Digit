using UnityEngine;
using UnityEngine.UI;

public class AgilityText : MonoBehaviour
{
    private Text text;
    public Stats stats;
    private string agility;
    private string current;
    private string leveling;

    void Start()
    {
        text = GetComponent<Text>();
        agility = "Agility: ";
        current = stats.vAgility.ToString("0");
        text.text = agility + stats.vAgility.ToString("0");
    }

    void Update()
    {
        current = stats.vAgility.ToString("0");
        if (Experience.MyInstance.isLvling)
        {
            leveling = Experience.MyInstance.agUp.ToString("0");
            if (Experience.MyInstance.agUp > 0)
            {
                text.text = agility + current + " +" + leveling;
            }
            else
            {
                text.text = agility + current;
            }
        }
        else
        {
            // Just show the text.
            text.text = agility + current;
        }
    }
}
