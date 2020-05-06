using UnityEngine;
using UnityEngine.UI;

public class LuckText : MonoBehaviour
{
    private Text text;
    public Stats stats;
    private string luck;
    private string current;
    private string leveling;

    void Start()
    {
        text = GetComponent<Text>();
        luck = "Luck: ";
        current = stats.vLuck.ToString("0");
        text.text = luck + stats.vLuck.ToString("0");
    }

    void Update()
    {
        current = stats.vLuck.ToString("0");
        if (Experience.MyInstance.isLvling)
        {
            leveling = Experience.MyInstance.lucUp.ToString("0");
            if (Experience.MyInstance.lucUp > 0)
            {
                text.text = luck + current + " +" + leveling;
            }
            else
            {
                text.text = luck + current;
            }
        }
        else
        {
            // Just show the text.
            text.text = luck + current;
        }
    }
}
