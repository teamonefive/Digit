using UnityEngine;
using UnityEngine.UI;

public class AgilityPlus : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    void Update()
    {
        // If we leveled, and not all points have been allocated. Show the button.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }

    public void AgilityUp()
    {
        // If we leveled, and not all points allocated, add one to stat and increase stat points.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            Experience.MyInstance.agUp += 1;
            Experience.MyInstance.statPoints++;
        }
    }
}
