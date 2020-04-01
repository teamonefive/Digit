using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fatigue : MonoBehaviour
{

    //public string scene;
    public Stats stat;
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


    public void updateFatigue(float fatigueDecrement)
    {
        stat.vFatigue -= fatigueDecrement;
        
    }

    public void checkFatigue()
    {
        if (stat.vFatigue < 1f)
        {
            GetComponent<TileBasedMover>().enabled = false;
            // transitionAnim.SetBool("End", false);
            transitionAnim.SetBool("TransitionFatigue", true);
            //new WaitForSeconds(1f);

            stat.vFatigue = 100f * (stat.vEndurance * 1.15f + 1f);

            Invoke("flipTheBool", 1.3f);
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
        GetComponent<TileBasedMover>().enabled = true;
    }

}
