using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipeManager", menuName = "CraftingRecipeManager", order = 2)]

public class CraftingRecipesManager : ScriptableObject
{
    public List<string> RecipeName = new List<string>();
    public List<CraftingRecipes> Recipe = new List<CraftingRecipes>();
}
