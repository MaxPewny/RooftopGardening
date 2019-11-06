using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GardenData", menuName = "ScriptableObjects/Garden", order = 1)]
public class GardenData : ScriptableObject
{
    public PlantCurrency Tomato = new PlantCurrency();
}
