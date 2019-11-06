using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    public Level PlantLevel;
    public CareState PlantCareState;
    public WaterLevel PlantWaterLevel;

    public int BugApperanceTime = 1;

    public int MaxFruitCounter;
    public int FruitsCounter;

    public string NextGrowthDate;
    public string NextWaterDate;
    public string NextBugDate;
}
