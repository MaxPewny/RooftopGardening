using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plant : MonoBehaviour, IPointerClickHandler
{
    public int GardenNumber;
    public int PlantNumber;

    public PlantPreset preset;

    public GameObject Bug;
    public GameObject FruitPrefab;
    public List<GameObject> Fruits = new List<GameObject>();

    public float BugApperanceTime = 1; // TODO: move to Scriptable Object?
    public float GrowCycleTime = 1;
    public float WaterCycleTime = 1;

    public bool BugIsThere = false;
    private bool hasFruits = false;
    private bool isRipe = false;

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

    public void LoadFromData(PlantData Data, PlantPreset Preset)
    {
        GardenNumber = Data.GardenNumber;
        PlantNumber = Data.SlotNumber;

        preset = Preset;

        ChangeSprite(Data.PlantLevel);
        SetFruits();
        GrowFruit(Data.FruitsCounter);
        BugAppearance(Data.BugIsThere);
    }

    public void SetData(PlantPreset Preset, PlantData Data) 
    {
        GardenNumber = Data.GardenNumber;
        PlantNumber = Data.SlotNumber;

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
                gameObject.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[0].UsedSprite;
                transform.localScale += preset.PlantObjects[0].Position;
                transform.localScale += preset.PlantObjects[0].Scale;
                gameObject.GetComponent<BoxCollider>().size += preset.PlantObjects[0].ColliderSize;
                break;
            case Level.LEVEL_2:
                gameObject.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[1].UsedSprite;
                transform.localScale += preset.PlantObjects[1].Position;
                transform.localScale += preset.PlantObjects[1].Scale;
                gameObject.GetComponent<BoxCollider>().size += preset.PlantObjects[1].ColliderSize;
                break;
            case Level.LEVEL_3:
                gameObject.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[2].UsedSprite;
                transform.localScale += preset.PlantObjects[2].Position;
                transform.localScale += preset.PlantObjects[2].Scale;
                gameObject.GetComponent<BoxCollider>().size += preset.PlantObjects[2].ColliderSize;
                isRipe = true;
                break;
            default:
                Debug.Log("Switch: no case");
                break;
        }
    }

    public void SetFruits() 
    {
        if (preset.FruitObjects.Count == 0)
        {
            hasFruits = false;
        }
        else 
        {
            hasFruits = true;
            foreach (var fruitPreset in preset.FruitObjects)
            {
                GameObject fruit = Instantiate(FruitPrefab, fruitPreset.Position, Quaternion.identity, transform);
                Fruits.Add(fruit);
                fruit.gameObject.SetActive(false);
            }
        }      
    }

    public void WaterPlant()
    {
        GameplayController.Instance.WaterPlant(GardenNumber, PlantNumber, WaterCycleTime);
    }

    public void FertilizePlant()
    {
        GameplayController.Instance.FertilizePlant(GardenNumber, PlantNumber, 1);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasFruits && isRipe)
        {
            GameplayController.Instance.FruitHarvested(GardenNumber, PlantNumber);
            GetComponentInParent<GardenSlot>().ResetPlantSlot();
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
