using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    public Dialogue digDialogue;
    public TileBasedMover tile;
    public GameObject Dwarf;
    private bool firstTime = true;
    void Update()
    {
        if (tile.moving == true && firstTime == true)
        {
            TriggerStartDialogue();
            firstTime = false;
        }
    }
    public void TriggerStartDialogue ()
	{
        Dwarf.GetComponent<TileBasedMover>().enabled = false;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void TriggerStartDigDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(digDialogue);
    }

}
