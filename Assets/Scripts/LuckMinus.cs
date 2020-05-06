using UnityEngine;
using UnityEngine.UI;

public class LuckMinus : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    void Update()
    {
        // If we leveled and a point was put in our stat, show minus button.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.lucUp > 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void LuckDown()
    {
        if (Experience.MyInstance.isLvling && Experience.MyInstance.lucUp > 0)
        {
            Experience.MyInstance.lucUp -= 1;
            Experience.MyInstance.statPoints--;
        }
    }
}

