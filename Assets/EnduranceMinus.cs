using UnityEngine;
using UnityEngine.UI;

public class EnduranceMinus : MonoBehaviour
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
        if (Experience.MyInstance.isLvling && Experience.MyInstance.endUp > 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void EnduranceDown()
    {
        if (Experience.MyInstance.isLvling && Experience.MyInstance.endUp > 0)
        {
            Experience.MyInstance.endUp -= 1;
            Experience.MyInstance.statPoints--;
        }
    }
}

