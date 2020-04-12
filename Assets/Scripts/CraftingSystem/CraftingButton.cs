using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingButton : MonoBehaviour
{

    [SerializeField] private Button button;
    [SerializeField] private Item1 _item;
    [SerializeField] private Text nameLabel;
    [SerializeField] private Text craftingRecipe;
    [SerializeField] private Image iconImage;
    [SerializeField] private int itemID;
    [SerializeField] private CraftingInfo _info;




    // Start is called before the first frame update
    void Start()
    {
        if(_item.MyTitle == "Iron Pickaxe" ) {
            IronPickAxe ironPickaxe = ( IronPickAxe )Instantiate(_item);
        }
        else if(_item.MyTitle == "Silver Pickaxe" ) {
            SilverPickAxe silverPickaxe = ( SilverPickAxe )Instantiate(_item);
        }
        else if(_item.MyTitle == "Gold Pickaxe" ) {
            GoldPickAxe goldPickaxe = ( GoldPickAxe )Instantiate(_item);
        }
        else if(_item.MyTitle == "Mithril Pickaxe" ) {
            DiamondPickAxe diamondPickaxe = ( DiamondPickAxe )Instantiate(_item);
        }
        nameLabel.text = _item.MyTitle;
        iconImage.sprite = _item.MyIcon;
        

    }
    public void DisplayInfo() {
        _info.updateItem(_item);
        //InventoryScript.MyInstance.AddItem(_item);
    }


    
}
