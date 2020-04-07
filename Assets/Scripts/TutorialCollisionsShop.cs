using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollisionsShop : MonoBehaviour
{
    // Start is called before the first frame update
    public bool trig2 = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player" && trig2 == true)
        {
            trig2 = false;
            //Dwarf.GetComponent<TileBasedMover>().enabled = false;
            FindObjectOfType<DialogueTrigger>().TriggerShopDialogue();
        }
    }
}
