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

    public void SetPressedL(bool valueL)
    {
        stat.pressedL = valueL;
    }



    // 2
    // If the button's state is pressed then we fire the
    // OnButtonHeld event.
    void Update()
    {
        if (stat.pressedL)
        {
            print("PRESSED");
            OnButtonHeldL.Invoke();
        }

    }
}