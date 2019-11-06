using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public PlantData data;

    public Level PlantLevel;
    public CareState PlantCareState;
    public WaterLevel PlantWaterLevel;

    public List<GameObject> Fruits;

    public int BugApperanceTime = 1;

    public int MaxFruitCounter;
    private int fruitsCounter;

    public DateTime NextGrowthDate;
    public DateTime NextWaterDate;
    public DateTime NextBugDate;

    public void LoadFromData(PlantData Data)
    {
        data = Data;

        PlantLevel = data.PlantLevel;
        PlantCareState = data.PlantCareState;
        PlantWaterLevel = data.PlantWaterLevel;
        BugApperanceTime = data.BugApperanceTime;
        MaxFruitCounter = data.MaxFruitCounter;
        fruitsCounter = data.FruitsCounter;

        NextGrowthDate = DateTime.Parse(data.NextGrowthDate);
        NextWaterDate = DateTime.Parse(data.NextWaterDate);
        NextBugDate = DateTime.Parse(data.NextBugDate);
    }

    public void SaveToData() 
    {
        data.PlantLevel = PlantLevel;
        data.PlantCareState = PlantCareState;
        data.PlantWaterLevel = PlantWaterLevel;
        data.BugApperanceTime = BugApperanceTime;
        data.MaxFruitCounter = MaxFruitCounter;
        data.FruitsCounter = fruitsCounter;

        data.NextGrowthDate = NextGrowthDate.ToString();
        data.NextWaterDate = NextWaterDate.ToString();
        data.NextBugDate = NextBugDate.ToString();
    }

    public void Check()
    {
        if (DateTime.Now >= NextWaterDate) NextGrowthDate += DateTime.Now - NextWaterDate;
        if (DateTime.Now >= NextBugDate) NextGrowthDate += DateTime.Now - NextBugDate;
        if (DateTime.Now >= NextGrowthDate) Growth();
    }

    public void WaterPlant()
    {

    }

    public void BugRemoved()
    {
        NextBugDate = DateTime.Now.AddHours(BugApperanceTime);
    }
       

    void Growth() 
    {
        PlantLevel++;
    }
}
