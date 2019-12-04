using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int GardenNumber;
    public int PlantNumber;

    public Sprite Level1Sprite;
    public Sprite Level2Sprite;
    public Sprite Level3Sprite;

    public GameObject Bug;
    public List<GameObject> Fruits = new List<GameObject>();

    public float BugApperanceTime = 1; // TODO: move to Scriptable Object?
    public float GrowCycleTime = 1;
    public float WaterCycleTime = 1;

    public bool BugIsThere = false;

    private void Awake()
    {
        foreach (Transform item in GetComponentsInChildren<Transform>() )
        {
            if (item.tag == "Fruit")
            {
                Fruits.Add(item.gameObject);
            }
            else if (item.tag == "Bug")
            {
                Bug = item.gameObject;
            }
        }
    }

    public void LoadFromData(PlantData Data)
    {
        GardenNumber = Data.GardenNumber;
        PlantNumber = Data.SlotNumber;

        ChangeSprite(Data.PlantLevel);
        GrowFruit(Data.FruitsCounter);
        BugAppearance(Data.BugIsThere);
    }

    public void SetData(PlantPreset Preset, PlantData Data) 
    {
        GardenNumber = Data.GardenNumber;
        PlantNumber = Data.SlotNumber;

        Level1Sprite = Preset.Level1Sprite;
        Level2Sprite = Preset.Level2Sprite;
        Level3Sprite = Preset.Level3Sprite;

        BugApperanceTime = Preset.BugApperanceTime;
        GrowCycleTime = Preset.GrowCycleTime;
        WaterCycleTime = Preset.WaterCycleTime;

        Data.PlantLevel = Level.LEVEL_1;
        Data.FruitsCounter = 0;
        Data.Type = Preset.Type;
        Data.GrowCycleTime = Preset.GrowCycleTime;
        Data.MaxFruitCounter = Preset.MaxFruitCounter;
        Data.NextBugDate = Preset.StartBugDate;
        Data.NextGrowthDate = Preset.StartGrowthDate;
        Data.NextWaterDate = Preset.StartWaterDate;

        Data.SlotUsed = true;

        ChangeSprite(Data.PlantLevel);
    }

    public void Check() 
    {
        PlantData data = GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber];
        ChangeSprite(data.PlantLevel);
        BugAppearance(data.BugIsThere);
        GrowFruit(data.FruitsCounter);
    }


    public void ChangeSprite(Level PlantLevel) 
    {
        switch (PlantLevel)
        {
            case Level.LEVEL_0: 
                break;
            case Level.LEVEL_1:
                gameObject.GetComponent<SpriteRenderer>().sprite = Level1Sprite;
                break;
            case Level.LEVEL_2:
                gameObject.GetComponent<SpriteRenderer>().sprite = Level2Sprite;
                break;
            case Level.LEVEL_3:
                gameObject.GetComponent<SpriteRenderer>().sprite = Level3Sprite;
                break;
            default:
                Debug.Log("Switch: no case");
                break;
        }
    }

    public void WaterPlant()
    {
        GameplayController.Instance.WaterPlant(GardenNumber, PlantNumber, WaterCycleTime);
    }

    public void BugRemoved()
    {
        GameplayController.Instance.BugRemoved(GardenNumber, PlantNumber, BugApperanceTime);
        BugIsThere = false;
    }

    public void FruitHarvested()
    {
        GameplayController.Instance.FruitHarvested(GardenNumber, PlantNumber);
    }

    public void BugAppearance(bool BugThere)
    {
        Bug.SetActive(BugThere);
        BugIsThere = BugThere;

        
    }

    public void GrowFruit(int Amount)
    {
        int amount = Amount;
        {
            for (int i = 0; i < Fruits.Count; i++)
            {
                if (Fruits[i].activeSelf == true)
                {
                    amount--;
                }
            }
            for(int i = 0; i < Fruits.Count; i++)
            {
                if (amount <= 0)
                {
                    return;
                }
                else if (Fruits[i].activeSelf == false)
                {
                    Fruits[i].SetActive(true);
                    amount--;
                }
            }

        }
        
    }

    //public void Check()
    //{
    //    if (DateTime.Now >= NextWaterDate) NextGrowthDate += DateTime.Now - NextWaterDate;
    //    if (DateTime.Now >= NextBugDate) 
    //    { 
    //        NextGrowthDate += DateTime.Now - NextBugDate;
    //        if(BugIsThere == false) BugAppearance();
    //    }
    //    if (DateTime.Now >= NextGrowthDate) 
    //    {
    //        if (PlantLevel < Level.LEVEL_3)
    //        {
    //            Growth();
    //        }
    //        else 
    //        {
    //            if (fruitsCounter < MaxFruitCounter)
    //            {
    //                fruitsCounter++;
    //                GrowFruit(1);
    //            }

    //        }

    //    }
    //}

    //void Growth() 
    //{
    //    PlantLevel++;
    //    ChangeSprite();
    //    NextWaterDate = DateTime.Now;
    //    NextGrowthDate = DateTime.Now.AddHours(GrowCycleTime);
    //}
}
