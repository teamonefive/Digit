using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollisionsDigging : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            //Dwarf.GetComponent<TileBasedMover>().enabled = false;
            FindObjectOfType<DialogueTrigger>().TriggerStartItemDialogue();
        }
    }
}
