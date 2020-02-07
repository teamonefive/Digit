using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
  public void Interact()
  {
    Debug.Log("interact");
  }

  public void StopInteract()
  {
    Debug.Log("Stop interact");
  }
}
