using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData 
{
    public int GardenNumber;
    public int SlotNumber;

    public bool SlotUsed = false;

    public PlantType Type;
    public Level PlantLevel;
    public CareState PlantCareState;
    public WaterLevel PlantWaterLevel;

    public int BugApperanceTime = 1;

    public int MaxFruitCounter;
    public int FruitsCounter;

    public bool BugIsThere = false;

    //public int MaxBugCounter;
    //public int BugCounter;

    public int GrowCycleTime;

    public string NextGrowthDate = "01.01.2000 12:00:00 ";
    public string NextWaterDate = "01.01.2000 12:00:00 ";
    public string NextBugDate = "01.01.2000 12:00:00 ";
}
