using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedPack", menuName = "ScriptableObjects/SeedPackPreset", order = 1)]
public class SeedPackPreset : ScriptableObject
{
    public PlantType Type;

    public Sprite Seed;

    public string Name;
}
