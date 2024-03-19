using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeName", menuName = "CraftingRecipeRequirement", order = 1)]

public class CraftingRecipes : ScriptableObject
{
    public List<ScriptableObject> RecipeRequirement = new List<ScriptableObject>();
}
