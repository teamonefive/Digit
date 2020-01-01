using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClick Handler
{
    [SerializeField] Image image;

    public event Action<Item> OnRightClickEVent;

    private Item _item;
    public Item Item {
        get { return _item; }
        set {
            _item = value;

            if(_item == null) {
                image.enabled = false;
            }
            else {
                image.sprite = _item.Icon;
                image.enabled = true;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
             if(Item != null && OnRightClickEVent != null)
            {
                OnRightClickEVent(Item);
            }
            
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
