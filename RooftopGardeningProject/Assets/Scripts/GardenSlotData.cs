using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GardenSlotData", menuName = "ScriptableObjects/GardenSlot", order = 1)]
public class GardenSlotData : ScriptableObject
{
    public PlantData Plant = null;
}
