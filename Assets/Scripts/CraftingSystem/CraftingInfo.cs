using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingInfo : MonoBehaviour
{
    public GameObject _view;
    public Stats _stats;
    public Item1 myItem;
    [SerializeField] private Text ItemName;
    [SerializeField] private Text ItemDescription;
    [SerializeField] private Image iconImage;
    [SerializeField] private int itemID;
    [SerializeField] private Button CraftingButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() { 
        if(myItem == null ) {
            CraftingButton.interactable = false;
        }
        else {
            CraftingButton.interactable = InventoryScript.MyInstance.IsCraftable(myItem.MyCraftingComponent, myItem.MyCraftingComponentQuantity) ? true : false;
        }
        
      

    }
    public void updateItem(Item1 item) {
        myItem = item;
        ItemName.text = myItem.MyTitle;
        if(myItem.MyQuality == Quality.Common ) {
            ItemName.color = Color.white;
        }
        else if(myItem.MyQuality == Quality.Uncommon ) {
            ItemName.color = Color.green;
        }
        else if(myItem.MyQuality == Quality.Rare ) {
            ItemName.color = Color.cyan;
        }
        else {
            ItemName.color = new Color(143f,0f,254f,1f);
        }
        ItemDescription.text = myItem.MyDescription;
        iconImage.sprite = myItem.MyIcon;
    }

    public void craftItem() {
        if (InventoryScript.MyInstance.IsCraftable(myItem.MyCraftingComponent, myItem.MyCraftingComponentQuantity)) {
            InventoryScript.MyInstance.Craft(myItem.MyCraftingComponent, myItem.MyCraftingComponentQuantity);
            InventoryScript.MyInstance.AddItem(myItem);
            _stats.itemsCrafted++;
            
        } 
    }

    public void closePanel() {
        _view.SetActive(false);
    }

}
