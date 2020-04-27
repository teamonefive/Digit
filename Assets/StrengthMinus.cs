using UnityEngine;
using UnityEngine.UI;

public class StrengthMinus : MonoBehaviour
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
        if (Experience.MyInstance.isLvling && Experience.MyInstance.strUp > 0)
        {
            button.interactable = true;
        } 
        else
        {
            button.interactable = false;
        }
    }

    public void StrengthDown()
    {
        if (Experience.MyInstance.isLvling && Experience.MyInstance.strUp > 0)
        {
            Experience.MyInstance.strUp -= 1;
            Experience.MyInstance.statPoints--;
        }
    }
}
