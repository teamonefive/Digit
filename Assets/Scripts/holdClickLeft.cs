using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class holdClickLeft : MonoBehaviour
{
    public TileBasedMover tile;
    public UnityEvent OnButtonHeldL;
    public Stats stat;
    float timer = 0f;

    public void SetPressedL(bool valueL)
    {
        stat.pressedL = valueL;
    }



    // 2
    // If the button's state is pressed then we fire the
    // OnButtonHeld event.
    void Update()
    {
        timer += Time.deltaTime;
        if (stat.pressedL && timer > 0.2f)
        {
            print("PRESSED");
            OnButtonHeldL.Invoke();
        }
        if (stat.pressedL == false)
            timer = 0f;

    }
}