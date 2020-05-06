using UnityEngine;
using UnityEngine.UI;

public class PerceptionText : MonoBehaviour
{
    private Text text;
    public Stats stats;
    private string perception;
    private string current;
    private string leveling;

    void Start()
    {
        text = GetComponent<Text>();
        perception = "Perception: ";
        current = stats.vPerception.ToString("0");
        text.text = perception + stats.vPerception.ToString("0");
    }

    void Update()
    {
        current = stats.vPerception.ToString("0");
        if (Experience.MyInstance.isLvling)
        {
            leveling = Experience.MyInstance.perUp.ToString("0");
            if (Experience.MyInstance.perUp > 0)
            {
                text.text = perception + current + " +" + leveling;
            }
            else
            {
                text.text = perception + current;
            }
        }
        else
        {
            // Just show the text.
            text.text = perception + current;
        }
    }
}
