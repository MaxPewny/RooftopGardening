using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Level PlantLevel;
    public CareState PlantCareState;
    public WaterLevel PlantWaterLevel;

    public List<GameObject> Fruits;
    
    public int maxFruitCounter;
    private int fruitsCounter;

    public DateTime NextGrowthDate;

    void Check() 
    {
        if (DateTime.Now >= NextGrowthDate) Growth();
    }

    void Growth() 
    {
        PlantLevel++;
    }
}
