using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    private Bag bag;

    [SerializeField]
    private Sprite full, empty;
    // Start is called before the first frame update

    public Bag MyBag
    {
        get
        {
            return bag;
        }

        set
        {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (bag != null)
        {
            bag.MyBagScript.OpenClose();
        }
    }
}
