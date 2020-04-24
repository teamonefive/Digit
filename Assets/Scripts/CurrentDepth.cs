using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDepth : MonoBehaviour
{
    public Stats stats;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        //TODO:Jon Why is this stored as +49?
        int depth = stats.currentDepth - 49;
        text.text = depth.ToString("0");
    }
}
