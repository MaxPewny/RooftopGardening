using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/PlantPreset", order = 1)]
public class PlantPreset : ScriptableObject 
{ 

    public PlantType Type;

    public Sprite Level1Sprite;
    public Sprite Level2Sprite;
    public Sprite Level3Sprite;

    public float BugApperanceTime = 1;
    public float GrowCycleTime = 1;
    public float WaterCycleTime = 1;

    public int MaxFruitCounter;

    public string StartGrowthDate { get { return DateTime.Now.AddHours(GrowCycleTime).ToString(); } }
    public string StartWaterDate { get { return DateTime.Now.AddHours(WaterCycleTime).ToString(); } }
    public string StartBugDate { get { return DateTime.Now.AddHours(BugApperanceTime).ToString(); } }

}
