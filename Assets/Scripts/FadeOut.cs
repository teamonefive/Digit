using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    void Start()
    {
        // Self destruct in T-Minus d
        DestroyObjectDelayed(1.0f);
    }

    void DestroyObjectDelayed(float d)
    {
        Destroy(gameObject, d);
    }
}
