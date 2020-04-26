using UnityEngine;
using UnityEngine.UI;

public class PerceptionMinus : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    void Update()
    {
        // If we leveled and a point was put in our stat, show minus button.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.perUp > 0)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
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

