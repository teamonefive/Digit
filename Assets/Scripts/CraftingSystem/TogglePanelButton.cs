using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanelButton : MonoBehaviour
{
    public GameObject _panel;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        _panel.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle() {
        active = !active;
        _panel.SetActive(active);
    }

}
