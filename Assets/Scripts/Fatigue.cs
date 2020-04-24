using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fatigue : MonoBehaviour
{
    public Stats stat;
    public Animator transitionAnim;
    public Animator dwarf;
    public TileBasedMover tileBasedMover;
    public bool trig3 = true;
    private Image image;

    void Start()
    {
        tileBasedMover = tileBasedMover.GetComponent<TileBasedMover>();
        image = GetComponent<Image>();
        transitionAnim.enabled = true;
        transitionAnim.SetBool("TransitionFatigue", false);
    }

    void flipTheBool()
    {
        transitionAnim.SetBool("TransitionFatigue", false);
    }

    void Update()
    {
        checkFatigue();
    }

    public void updateFatigue(float fatigueDecrement)
    {
        stat.vFatigue -= fatigueDecrement;
        
    }

    public void checkFatigue()
    {
        if (stat.vFatigue < 1f)
        {
            tileBasedMover.enabled = false;

            image.enabled = true;

            transitionAnim.SetBool("TransitionFatigue", true);

            stat.vFatigue = stat.maxFatigue;
            
            stat.totalFatigues++;

            Invoke("flipTheBool", 1.3f);

            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        dwarf.SetBool("isFatigued", true);

        yield return new WaitForSeconds(5);

        dwarf.SetBool("isFatigued", false);

        image.enabled = false;

        tileBasedMover.enabled = true;

        if(trig3 == true)
        {
            trig3 = false;
            FindObjectOfType<DialogueTrigger>().TriggerFatigueDialogue();
        }
    }

}
