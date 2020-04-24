using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnduranceBar : MonoBehaviour
{
    public Stats stat;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = stat.vFatigue;
        slider.maxValue = stat.maxFatigue;
    }
}
