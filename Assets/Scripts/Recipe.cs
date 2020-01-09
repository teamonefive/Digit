using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe {
    public int[] m_requiredItems;
    public int m_itemToCraft;

    public Recipe(int itemToCraft, int[] requiredItems) {
        m_itemToCraft = itemToCraft;
        m_requiredItems = requiredItems;
    }
}
