using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public Dialogue digDialogue;
    public Dialogue collectFirstItem;
    public Dialogue visitTheShop;
    public Dialogue passedOut;
    public Dialogue lvlUp;
    public Dialogue crafting;
    public TileBasedMover tile;
    public GameObject Dwarf;
    private bool firstTime = true;
    void Awake()
    {
        //tile.oldPos = new Vector3(0, 0, 0);
        Dwarf.GetComponent<TileBasedMover>().enabled = false;
    }
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f && firstTime == true)
        {
            Dwarf.GetComponent<TileBasedMover>().enabled = true;
            //tile.targetPos += new Vector3(-1, 0, 0f);
        }

        if (tile.moving == true && firstTime == true)
        {
            TriggerStartDialogue();
            firstTime = false;
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
}
