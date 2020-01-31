using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DepthLighting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dwarf;
    private Light2D lightScript;
    public Stats stat;

    void Start()
    {
        lightScript = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float yPos = dwarf.transform.position.y;
        if (yPos >= 0)
        {
            lightScript.intensity = 0.5f;
        }
        else if (yPos <= (-151 - 2*stat.vPerception))
        {
            lightScript.intensity = 0.0f;
        }
        else
        {
            lightScript.intensity = (((151+(2*stat.vPerception)) + yPos) / (151+(2*stat.vPerception)) * 0.5f);
        }
    }
}
