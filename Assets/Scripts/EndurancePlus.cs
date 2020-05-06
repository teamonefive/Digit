using UnityEngine;
using UnityEngine.UI;

public class EndurancePlus : MonoBehaviour
{
    private Button image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Button>();
        image.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If we leveled, and not all points have been allocated. Show the button.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            image.interactable = true;
        }
        else
        {
            image.interactable = false;
        }
    }

    public void EnduranceUp()
    {
        // If we leveled, and not all points allocated, add one to stat and increase stat points.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            Experience.MyInstance.endUp += 1;
            Experience.MyInstance.statPoints++;
        }
    }
}
