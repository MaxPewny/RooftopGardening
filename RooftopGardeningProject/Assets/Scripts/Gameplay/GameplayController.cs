using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using core;

public class GameplayController : Singleton<GameplayController>
{
    protected GameplayController() { }
    //public static GameplayController Instance;

    public List<PlantCurrency> PlantCurrencies = new List<PlantCurrency>();

    public List<GardenData> GardenDatas = new List<GardenData>();

    public List<List<PlantData>> PlantDatas = new List<List<PlantData>>();


    public int GardenSpotsCount = 1;
    public int PlantsPerSpotCount = 3;

    private void Awake()
    {
        //The cast to(PlantType[]) is not strictly necessary, but does make the code 0.5 ns faster.
        foreach (PlantType plant in (PlantType[])Enum.GetValues(typeof(PlantType)))
        {
            PlantCurrencies.Add(new PlantCurrency(plant));
        }

        for (int i = 0; i < GardenSpotsCount; i++)
        {
            PlantDatas.Add(new List<PlantData>());
            GardenDatas.Add(new GardenData());
            GardenDatas[i].GardenNumber = i;
            GardenDatas[i].NextWeedGrowthDate = DateTime.Now.AddHours(GardenDatas[i].WeedAppearanceTimer).ToString() ;

            for (int j = 0; j < PlantsPerSpotCount; j++)
            {
                PlantDatas[i].Add(new PlantData());
                PlantDatas[i][j].GardenNumber = i;
                PlantDatas[i][j].FruitsCounter = j;
            }
        }

        
    }

    public void FixedUpdate() 
    {
        for (int i = 0; i < PlantDatas.Count; i++)
        {
            foreach (PlantData data in PlantDatas[i])
            {
                if (data.SlotUsed == true)
                {
                    CheckPlant(data);
                }
            } 
        }

        foreach (GardenData data in GardenDatas)
        {
            CheckGarden(data);
        }
    }

    public void CheckPlant(PlantData UsedData)
    {
        if (DateTime.Now >= DateTime.Parse(UsedData.NextWaterDate)) 
        {
            DateTime temporaryDate = new DateTime();
            temporaryDate = DateTime.Parse(UsedData.NextGrowthDate);
            temporaryDate += DateTime.Now - DateTime.Parse(UsedData.NextWaterDate);
            UsedData.NextGrowthDate = temporaryDate.ToString();
        }
        if (DateTime.Now >= DateTime.Parse(UsedData.NextBugDate))
        {
            DateTime temporaryDate = new DateTime();
            temporaryDate = DateTime.Parse(UsedData.NextGrowthDate);
            temporaryDate += DateTime.Now - DateTime.Parse(UsedData.NextBugDate);
            UsedData.BugIsThere = true;
            //if (UsedData.BugCounter < UsedData.MaxBugCounter)
            //{
            //    UsedData.BugCounter++;
            //}
        }
        if (DateTime.Now >= DateTime.Parse(UsedData.NextGrowthDate))
        {
            if (UsedData.PlantLevel < Level.LEVEL_3)
            {
                UsedData.NextWaterDate = DateTime.Now.ToString();
                UsedData.NextGrowthDate = DateTime.Now.AddHours(UsedData.GrowCycleTime).ToString();
                UsedData.PlantLevel++;
            }
            else
            {
                if (UsedData.FruitsCounter < UsedData.MaxFruitCounter)
                {
                    UsedData.FruitsCounter++;
                }

            }

        }
    }

    public void CheckGarden(GardenData UsedData) 
    {

        if (DateTime.Now >= DateTime.Parse(UsedData.NextWeedGrowthDate))
        {
            UsedData.WeedCounter += UnityEngine.Random.Range(0, 4);
            if (UsedData.WeedCounter > UsedData.MaxWeedCounter)
            {
                UsedData.WeedCounter = UsedData.MaxWeedCounter;
            }
        }
    }

    public void WaterPlant(int GardenNr, int PlantNr, float WaterCycleTime)
    {
        for (int i = 0; i < PlantDatas.Count; i++)
        {
            foreach (PlantData data in PlantDatas[i])
            {
                if (data.GardenNumber == GardenNr && data.SlotNumber == PlantNr)
                {
                    data.NextWaterDate = DateTime.Now.AddHours(WaterCycleTime).ToString();
                    return;
                }
            }
        }
    }

    public void FertilizePlant(int GardenNr, int PlantNr, float FertilizerTime)
    {
        for (int i = 0; i < PlantDatas.Count; i++)
        {
            foreach (PlantData data in PlantDatas[i])
            {
                if (data.GardenNumber == GardenNr && data.SlotNumber == PlantNr)
                {
                    TimeSpan time = DateTime.Parse(data.NextGrowthDate) - DateTime.Now;
                    //time = time.;
                    //data.NextGrowthDate = DateTime.Now.AddHours(WaterCycleTime).ToString();

                    return;
                }
            }
        }
    }

    public void BugRemoved(int GardenNr, int PlantNr, float BugApperanceTime)
    {
        for (int i = 0; i < PlantDatas.Count; i++)
        {
            foreach (PlantData data in PlantDatas[i])
            {
                if (data.GardenNumber == GardenNr && data.SlotNumber == PlantNr)
                {
                    data.NextBugDate = DateTime.Now.AddHours(BugApperanceTime).ToString();
                    data.BugIsThere = false;
                    //data.BugCounter--;
                    return;
                }
            }
        }
    }

    public void FruitHarvested(int GardenNr, int PlantNr)
    {
        for (int i = 0; i < PlantDatas.Count; i++)
        {
            foreach (PlantData data in PlantDatas[i])
            {
                if (data.GardenNumber == GardenNr && data.SlotNumber == PlantNr)
                {   
                    foreach (PlantCurrency currency in PlantCurrencies)
                    {
                        if (currency.Plant == data.Type)
                        {
                            currency.Fruit++;
                            data.FruitsCounter--;
                            return;
                        }
                    }
                    return;
                }
            }
        }
    }

    public void ResetData(int GardenNr, int PlantNr)
    {
        for (int i = 0; i < PlantDatas.Count; i++)
        {
            foreach (PlantData data in PlantDatas[i])
            {
                if (data.GardenNumber == GardenNr && data.SlotNumber == PlantNr)
                {                     
                    data.SlotUsed = false;

                    data.PlantLevel = Level.LEVEL_0;
                    data.MaxFruitCounter = 0;
                    data.FruitsCounter = 0;

                    data.BugIsThere = false;

                    data.GrowCycleTime = 0;

                    data.NextGrowthDate = "01.01.2000 12:00:00 ";
                    data.NextWaterDate = "01.01.2000 12:00:00 ";
                    data.NextBugDate = "01.01.2000 12:00:00 ";
                }
            }
        }
    }
}
