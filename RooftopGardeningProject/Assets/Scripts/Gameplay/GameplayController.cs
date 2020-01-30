﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using core;

public class GameplayController : Singleton<GameplayController>
{
    protected GameplayController() { }
    //public static GameplayController Instance;

    public List<PlantCurrency> PlantCurrencies = new List<PlantCurrency>();

    public List<NeighborData> NeighborDatas = new List<NeighborData>();

    public List<GardenData> GardenDatas = new List<GardenData>();

    public List<List<PlantData>> PlantDatas = new List<List<PlantData>>();

    public List<String> FavoritedRecipes = new List<string>();

    //public List<FruitData> FruitDatas = new List<FruitData>();

    public string CurrentSceneName;

    public float PlayerXp;
    public Level PlayerLevel = Level.LEVEL_1;
    public float MaxPlayerXp;


    public int GardenSpotsCount = 2;
    public int PlantsPerSpotCount = 3;

    private void Awake()
    {
        MaxPlayerXp = 300 * (int)PlayerLevel;
        if (MaxPlayerXp < 300)
        {
            MaxPlayerXp = 300;
        }

        //The cast to(PlantType[]) is not strictly necessary, but does make the code 0.5 ns faster.
        foreach (PlantType plant in (PlantType[])Enum.GetValues(typeof(PlantType)))
        {
            PlantCurrencies.Add(new PlantCurrency(plant));
        }

        foreach (Neighbor neighbor in (Neighbor[])Enum.GetValues(typeof(Neighbor)))
        {
            NeighborDatas.Add(new NeighborData(neighbor));
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
                PlantDatas[i][j].SlotNumber = j;
            }
        }
    }

    public void Update()
    {
        LevelUp();

        foreach (List<PlantData> list in PlantDatas)
        {
            foreach (PlantData data in list)
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

        foreach (NeighborData data in NeighborDatas)
        {
            CheckNeighbours(data);
        }

        //foreach (FruitData data in FruitDatas)
        //{
        //    CheckFruit(data);
        //}
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
            if (UsedData.PlantLevel < UsedData.MaxPlantLevel)
            {
                UsedData.NextWaterDate = DateTime.Now.ToString();
                UsedData.NextGrowthDate = DateTime.Now.AddHours(UsedData.GrowCycleTime).ToString();
                Notification.Instance.SendAndroidNotification(DateTime.Now.AddHours(UsedData.GrowCycleTime), "Rooftop Garden", "Hey dein Pflanzen wachsen, sieh sie dir an");
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
            UsedData.WeedCounter += UnityEngine.Random.Range(0, 2);
            if (UsedData.WeedCounter > UsedData.MaxWeedCounter)
            {
                UsedData.WeedCounter = UsedData.MaxWeedCounter;
            }
        }
    }

    public void CheckNeighbours(NeighborData UsedData)
    {
        if (UsedData.NeighborXp >= UsedData.MaxXp)
        {
            ++UsedData.NeighborLevel;

            UsedData.NeighborXp = UsedData.NeighborXp - UsedData.MaxXp;

            UsedData.MaxXp = 300 * ((int)UsedData.NeighborLevel + 1);

            if (UsedData.MaxXp < 300)
            {
                UsedData.MaxXp = 300;
            }


            if (UsedData.NeighborXp < 0)
            {
                UsedData.NeighborXp = 0;
            }

        }
    }

    public void LevelUp()
    {
        if (PlayerXp >= MaxPlayerXp)
        {
            ++PlayerLevel;

            PlayerXp = PlayerXp - MaxPlayerXp;

            MaxPlayerXp = 300 * ((int)MaxPlayerXp + 1);


            if (PlayerXp < 0)
            {
                PlayerXp = 0;
            }
        }
    }


    //public void CheckFruit(FruitData UsedData)
    //{
    //
    //    if (!UsedData.IsRipe && DateTime.Now >= DateTime.Parse(UsedData.NextGrowthDate))
    //    {
    //        UsedData.IsRipe = true;
    //        
    //    }
    //}

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
                    data.WasCut = false;

                    data.GrowCycleTime = 0;

                    data.NextGrowthDate = "01.01.2000 12:00:00 ";
                    data.NextWaterDate = "01.01.2000 12:00:00 ";
                    data.NextBugDate = "01.01.2000 12:00:00 ";
                }
            }
        }
    }
}
