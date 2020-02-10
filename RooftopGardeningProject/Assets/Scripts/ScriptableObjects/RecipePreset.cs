using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/RecipePreset", order = 1)]
public class RecipePreset : ScriptableObject
{
    public Neighbor GivingNeighbor;

    public Level RequiredLevel;

    public RecipeType Type;

    public Sprite Icon;

    public Color UsedColor;

    public bool IsVegan;

    public string Name;

    public List<string> Ingredients;

    public string Description;
}
