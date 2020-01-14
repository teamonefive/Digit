using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    
    public void Terminate() {
        Debug.Log("Game was Terminated");
        Application.Quit();
    }
}
