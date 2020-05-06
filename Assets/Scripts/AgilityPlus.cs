using UnityEngine;
using UnityEngine.UI;

public class AgilityPlus : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    void Update()
    {
        // If we leveled, and not all points have been allocated. Show the button.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
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
