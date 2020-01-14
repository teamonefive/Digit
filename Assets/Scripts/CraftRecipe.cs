using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipe {
    public int[] m_requiredItems;
    public int m_itemToCraft;
    public CraftRecipe(int itemToCraft, int[] requiredItems) {
        m_requiredItems = requiredItems; m_itemToCraft = itemToCraft;
    }
}
