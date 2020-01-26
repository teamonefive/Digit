using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UICraftResult : MonoBehaviour, IPointerDownHandler
{
    public SlotPanel slotPanel;

    public void OnPointerDown(PointerEventData eventData) {
        slotPanel.EmptyAllSlots();
    }
}
