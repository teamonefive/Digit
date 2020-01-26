using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlotPanel : MonoBehaviour {
    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private UIItem craftResultSlot;
    private List<UIItem> uiItems = new List<UIItem>();
    
    
    public void UpdateRecipe() {
        int[] itemTable = new int[uiItems.Count];
        for(int i = 0; i < uiItems.Count; i++ ) {
            if(uiItems[i].m_item != null ) {
                itemTable[i] = uiItems[i].m_item.m_id;
            }
        }
        Item itemToCraft = recipeDatabase.CheckRecipe(itemTable);
        UpdateCraftingResultSlot(itemToCraft);
    }
    void UpdateCraftingResultSlot(Item itemToCraft) {
        craftResultSlot.UpdateItem(itemToCraft);
    }

    void Start()
    {
        uiItems = GetComponent<SlotPanel>().m_uiItems;
        uiItems.ForEach(i => i.isCraftingSlot = true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
