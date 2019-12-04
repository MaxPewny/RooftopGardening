using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NeighborTask", menuName = "ScriptableObjects/TaskPreset", order = 1)]
public class NeighborTaskPreset : ScriptableObject
{
    [System.Serializable]
    public class ObjectivePair
    {
        public PlantType Type;
        public float Count;
    }

    public string Name;

    public string Description;

    public float RewardedXP;

    public List<ObjectivePair> Objectives;
}