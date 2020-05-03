using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Dialogue dialogue;
    public Dialogue digDialogue;
    public Dialogue collectFirstItem;
    public Dialogue visitTheShop;
    public Dialogue passedOut;
    public Dialogue lvlUp;
    public Dialogue crafting;
    public Dialogue statboard;
    public TileBasedMover tile;
    public GameObject Dwarf;
    public Stats stat;
  
    void Awake()
    {
        //tile.oldPos = new Vector3(0, 0, 0);
        Dwarf.GetComponent<TileBasedMover>().enabled = false;
    }
    void Update()
    {
        if ((tile.dir != new Vector2 (1f, 0f) || tile.dir != new Vector2(-1f, 0f)) && stat.firstPlayTrig == true)
        {
            Dwarf.GetComponent<TileBasedMover>().enabled = true;
            //tile.targetPos += new Vector3(-1, 0, 0f);
        }

        if (tile.moving == true && stat.firstPlayTrig == true)
        {
            TriggerStartDialogue();
            stat.firstPlayTrig = false;
        }
    }
    public void TriggerStartDialogue()
    {
        Dwarf.GetComponent<TileBasedMover>().enabled = false;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void TriggerStartDigDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(digDialogue);
    }

    public void TriggerStartItemDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(collectFirstItem);
    }

    public void TriggerShopDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(visitTheShop);

    }

    public void TriggerFatigueDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(passedOut);

    }

    public void TriggerLvlUpDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(lvlUp);
    }
    public void TriggerCraftingDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(crafting);
    }
    public void TriggerStatsDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(statboard);
    }
}
