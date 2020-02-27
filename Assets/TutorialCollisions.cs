using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollisions : MonoBehaviour
{
    public GameObject Dwarf;
    public TileBasedMover tile;
    public bool moveRight = true;
    //public Collider2D Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MineEntrance()
    {
        Dwarf.GetComponent<TileBasedMover>().enabled = true;

    }
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            //Dwarf.GetComponent<TileBasedMover>().enabled = false;
            FindObjectOfType<DialogueTrigger>().TriggerStartDigDialogue();
        }
    }
}
