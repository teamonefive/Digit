using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollisions : MonoBehaviour
{
    public GameObject Dwarf;
    public TileBasedMover tile;
    public Stats stat;
    //public Collider2D Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Dwarf.GetComponent<TileBasedMover>().enabled = false;
  //      if (Input.GetAxisRaw("Horizontal") > 0f && moveRight == false)
    //    {
      //      Dwarf.GetComponent<TileBasedMover>().enabled = true;
        //    tile.targetPos += new Vector3(1, 0, 0f);
            
        //}
    }
    void OnTriggerEnter2D(Collider2D Player)
    {
       // moveRight = true;
        if (Player.gameObject.tag == "Player" && stat.mineEntranceTrig == true)
        {
            //Dwarf.GetComponent<TileBasedMover>().enabled = false;
            stat.mineEntranceTrig = false;
            FindObjectOfType<DialogueTrigger>().TriggerStartDigDialogue();
        }
    }
}
