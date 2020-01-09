using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RecipeDatabase : MonoBehaviour {
    public List<Recipe> m_recipes = new List<Recipe>();
    private ItemDatabase m_itemDatabase;

    private void Awake() {
        m_itemDatabase = GetComponent<ItemDatabase>();
    }

    private void Start() {
        BuildRecipeDatabase();
    }

    public Item CheckRecipe(int[] recipe) {
        foreach(Recipe r in m_recipes ) {
            if ( r.m_requiredItems.SequenceEqual(recipe) ) {
                return m_itemDatabase.GetItem(r.m_itemToCraft);
            }
        }
        return null;
    }

    void BuildRecipeDatabase() {
        m_recipes = new List<Recipe>() {
            new Recipe(1,
            new int[] {
                2,3
            }),
            new Recipe(3,
            new int[] {
                2,2
            })
        };
    }
}
