using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipeManager", menuName = "CraftingRecipeManager", order = 2)]

public class CraftingRecipesManager : ScriptableObject
{
    public List<string> recipeName = new List<string>();
    public List<CraftingRecipes> _recipe = new List<CraftingRecipes>();
}
