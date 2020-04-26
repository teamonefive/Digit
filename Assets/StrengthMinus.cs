using UnityEngine;
using UnityEngine.UI;

public class StrengthMinus : MonoBehaviour
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
        if (Experience.MyInstance.isLvling && Experience.MyInstance.strUp > 0)
        {
            image.enabled = true;
        } 
        else
        {
            image.enabled = false;
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
