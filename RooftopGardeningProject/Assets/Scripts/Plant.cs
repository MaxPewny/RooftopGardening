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

    public int BugApperanceTime = 1; // TODO: move to Scriptable Object?
    public int GrowCycleTime = 1;
    public int WaterCycleTime = 1;

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

    void BugAppearance(bool BugThere)
    {
        Bug.SetActive(BugThere);
        BugIsThere = BugThere;

        
    }

    void GrowFruit(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            Fruits[i].SetActive(true);
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
