using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel_Inventory;
    public GameObject Panel_Bagbar;

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
        InventoryScript inventory = InventoryScript.MyInstance;
        if (inventory.transform.position.z == 0)
        {
            inventory.transform.position = new Vector3(inventory.transform.position.x, inventory.transform.position.y, 1500f);
        }
        else
        {
            inventory.transform.position = new Vector3(inventory.transform.position.x, inventory.transform.position.y, 0f);

        }
    }

    public void OpenPanel2()
    {
        if (Panel_Bagbar != null)
        {
            bool isActive = Panel_Bagbar.activeSelf;
            Panel_Bagbar.SetActive(!isActive);
        }
    }
}
