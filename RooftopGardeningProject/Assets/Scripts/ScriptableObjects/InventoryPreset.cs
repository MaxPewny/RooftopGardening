using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/InventoryPreset", order = 1)]
public class InventoryPreset : ScriptableObject
{
    public PlantType Type;

    public Sprite Icon;

    public string Name;
}
