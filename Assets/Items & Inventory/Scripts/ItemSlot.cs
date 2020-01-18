using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;

    public event Action<Item1> OnRightClickEvent;

    private Item1 _item;
    public Item1 Item1
    {
        get { return _item; }
        set {
            _item = value;
            if(_item ==null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.Icon;
                image.enabled = true;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item1 != null && OnRightClickEvent != null)
                OnRightClickEvent(Item1);
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
