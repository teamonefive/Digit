using UnityEngine;
using UnityEngine.UI;

public class PerceptionMinus : MonoBehaviour
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
        if (Experience.MyInstance.isLvling && Experience.MyInstance.perUp > 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void PerceptionDown()
    {
        if (Experience.MyInstance.isLvling && Experience.MyInstance.perUp > 0)
        {
            Experience.MyInstance.perUp -= 1;
            Experience.MyInstance.statPoints--;
        }
    }
}

