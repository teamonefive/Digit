using UnityEngine;
using UnityEngine.UI;

public class ConfirmStatsButton : MonoBehaviour
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
        // If we leveled and all points are allocated. Show confirm button.
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints == 3)
        {
            image.enabled = true;
        } else
        {
            image.enabled = false;
        }
    }

    public void ConfirmStats()
    {
        if (Experience.MyInstance.isLvling && Experience.MyInstance.statPoints == 3)
        {
            Experience.MyInstance.confirm();
        } // else
        //TODO:Jon show some error about allocating all points?
    }
}
