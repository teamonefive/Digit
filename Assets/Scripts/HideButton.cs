using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 revealPos;
    public Vector3 revealPos2;
    public TileBasedMover dwarf;
    public GameObject button;
    public GameObject buttonWindow;
    public Stats stat;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dwarf.oldPos == revealPos || dwarf.oldPos == revealPos2)
        {
            button.SetActive(true);
            if(stat.craftTrig == true && (dwarf.oldPos == new Vector3(-49.5f, -1f, 0f) || dwarf.oldPos == new Vector3(-48.5f, -1f, 0f)))
            {
                FindObjectOfType<DialogueTrigger>().TriggerCraftingDialogue();
                stat.craftTrig = false;
            }
            if (stat.shopTrig == true && dwarf.oldPos == new Vector3(-59.5f, -1f, 0f))
            {
                FindObjectOfType<DialogueTrigger>().TriggerShopDialogue();
                stat.shopTrig = false;
            }
        }
        else
        {
            button.SetActive(false);
            buttonWindow.SetActive(false);
        }
    }
}
