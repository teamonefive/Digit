using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class holdClickDown : MonoBehaviour
{
    public TileBasedMover tile;
    public UnityEvent OnButtonHeldD;
    public Stats stat;

    public void SetPressedD(bool valueD)
    {
        stat.pressedD = valueD;
    }



    // 2
    // If the button's state is pressed then we fire the
    // OnButtonHeld event.
    void Update()
    {
        if (stat.pressedD)
        {
            print("PRESSED");
            OnButtonHeldD.Invoke();
        }

    }
}