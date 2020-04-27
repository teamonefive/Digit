using UnityEngine;
using UnityEngine.UI;

public class PerceptionPlus : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    // Update is called once per frame
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

    public void PerceptionUp()
    {
        // If we leveled, and not all points allocated, add one to stat and increase stat points.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            Experience.MyInstance.perUp += 1;
            Experience.MyInstance.statPoints++;
        }
    }
}
