using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButtonRest : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 revealPos;
    public Vector3 revealPos2;
    public TileBasedMover dwarf;
    public GameObject restButton;
    public Stats stat;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dwarf.oldPos == revealPos || dwarf.oldPos == revealPos2)
        {
            restButton.SetActive(true);
            if(stat.restTrig == true)
            {
                FindObjectOfType<DialogueTrigger>().TriggerRestDialogue();

                stat.restTrig = false;
            }
        }
        else
        {
            stat.restTrig = true;
            restButton.SetActive(false);
        }
    }
}
