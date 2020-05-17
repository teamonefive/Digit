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

    float timer = 0f;


    public void SetPressedD(bool valueD)
    {
        stat.pressedD = valueD;
    }



    // 2
    // If the button's state is pressed then we fire the
    // OnButtonHeld event.
    void Update()
    {
        timer += Time.deltaTime;
        if (stat.pressedD && timer > 0.5f)
        {
            print("PRESSED");
            OnButtonHeldD.Invoke();
        }
        if (stat.pressedD == false)
            timer = 0f;

    }
}