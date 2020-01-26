using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fatigue : MonoBehaviour
{
    public float vFatigue = 100f;
    //public string scene;
    public Animator transitionAnim;
    public TileBasedMover tile;


    // Start is called before the first frame update
    void Start()
    {
        transitionAnim.enabled = true;
        transitionAnim.SetBool("TransitionFatigue", false);
    }
    //void onMovement()
    //{
    //    vFatigue -= Time.deltaTime;
    //    vFatigue = Mathf.Clamp(vFatigue, 0, 100);
    //}

    void flipTheBool()
    {
        transitionAnim.SetBool("TransitionFatigue", false);
    }


    // Update is called once per frame
    public void Update()
    {

        if(tile.isDestroyedBlock == true)
        {
            vFatigue -= 10f;
        }
        tile.isDestroyedBlock = false;
        if (vFatigue < 1f)
        {
           // transitionAnim.SetBool("End", false);
            transitionAnim.SetBool("TransitionFatigue", true);
            //new WaitForSeconds(1f);
            

            vFatigue = 100f;

            Invoke("flipTheBool", 1.3f);
        }
    }

}
