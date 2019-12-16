using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/PlantPreset", order = 1)]
public class PlantPreset : ScriptableObject 
{

    [System.Serializable]
    public class PlantObject
    {
        public Level ObjectLevel;
        public Sprite UsedSprite;
        public Vector3 Position;
        public Vector3 Scale;
        public Vector3 ColliderSize;
    }

    [System.Serializable]
    public class FruitObject
    {
        public Sprite RipeSprite;
        public Sprite UnripeSprite;
        public Vector3 Position;
        public Vector3 Scale;
        public Vector3 ColliderSize;
    }

    public PlantType Type;
    public Level MaxLevel;

    public List<PlantObject> ExtraObjects;
    public List<PlantObject> PlantObjects;
    public List<FruitObject> FruitObjects;

    public float BugApperanceTime = 1;
    public float GrowCycleTime = 1;
    public float WaterCycleTime = 1;
    public float FruitGrowthTime = 1;

    public int MaxFruitCounter;

    public string StartGrowthDate { get { return DateTime.Now.AddHours(GrowCycleTime).ToString(); } }
    public string StartWaterDate { get { return DateTime.Now.AddHours(WaterCycleTime).ToString(); } }
    public string StartBugDate { get { return DateTime.Now.AddHours(BugApperanceTime).ToString(); } }

}
