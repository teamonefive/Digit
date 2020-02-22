using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }

    public void OpenPanel1()
    {
        if (Panel1 != null)
        {
            bool isActive = Panel1.activeSelf;
            Panel1.SetActive(!isActive);
        }
    }

    public void OpenPanel2()
    {
        if (Panel2 != null)
        {
            bool isActive = Panel2.activeSelf;
            Panel2.SetActive(!isActive);
        }
    }

    public void OpenPanel3()
    {
        if (Panel3 != null)
        {
            bool isActive = Panel3.activeSelf;
            Panel3.SetActive(!isActive);
        }
    }
}
