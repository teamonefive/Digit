using UnityEngine;
using UnityEngine.UI;

public class StrengthPlus : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If we leveled, and not all points have been allocated. Show the button.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            image.enabled = true;
        } else
        {
            image.enabled = false;
        }
    }

    public void StrengthUp()
    {
        // If we leveled, and not all points allocated, add one to stat and increase stat points.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints < 3)
        {
            Experience.MyInstance.strUp += 1;
            Experience.MyInstance.statPoints++;
        }
    }
}
