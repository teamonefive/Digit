using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;

    private Item1 _item1;
    public Item1 Item1
    {
        get { return _item1; }
        set
        {
            _item1 = value;

            if(_item1 == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item1.Icon;
                image.enabled = true;
            }

        }
    }

    private void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

    }
}
