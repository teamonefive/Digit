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

    public void SetPressedR(bool valueR)
    {
        stat.pressedR = valueR;
    }



    // 2
    // If the button's state is pressed then we fire the
    // OnButtonHeld event.
    void Update()
    {
        if (stat.pressedR)
        {
            print("PRESSED");
            OnButtonHeldR.Invoke();
        }

    }
}