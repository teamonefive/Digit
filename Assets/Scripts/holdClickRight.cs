using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class holdClickRight : MonoBehaviour
{
    public TileBasedMover tile;
    public UnityEvent OnButtonHeldR;
    public Stats stat;
    float timer = 0f;

    public void SetPressedR(bool valueR)
    {
        stat.pressedR = valueR;
    }



    // 2
    // If the button's state is pressed then we fire the
    // OnButtonHeld event.
    void Update()
    {
        timer += Time.deltaTime;
        if (stat.pressedR && timer > 0.2f)
        {
            print("PRESSED");
            OnButtonHeldR.Invoke();
        }
        if (stat.pressedR == false)
            timer = 0f;
    }
}