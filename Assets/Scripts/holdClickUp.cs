using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class holdClickUp : MonoBehaviour
{
    public TileBasedMover tile;
    public UnityEvent OnButtonHeldU;
    public Stats stat;
    float timer = 0f;

    public void SetPressedU(bool valueU)
    {
        stat.pressedU = valueU;
    }



    // 2
    // If the button's state is pressed then we fire the
    // OnButtonHeld event.
    void Update()
    {
        timer += Time.deltaTime;
        if (stat.pressedU && timer > 0.2f)
        {
            print("PRESSED");
            OnButtonHeldU.Invoke();
        }
        if (stat.pressedD == false)
            timer = 0f;
    }
}