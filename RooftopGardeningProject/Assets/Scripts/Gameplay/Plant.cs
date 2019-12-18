﻿using System;
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
    public GameObject PlantDisplay;
    public GameObject FruitPrefab;
    public GameObject ExtraPrefab;
    public List<GameObject> Fruits = new List<GameObject>();

    public float BugApperanceTime = 1; // TODO: move to Scriptable Object?
    public float GrowCycleTime = 1;
    public float WaterCycleTime = 1;

    private Level plantLevel = Level.LEVEL_0;
    private int fruitAmount;
    private bool bugIsThere = false;
    private bool hasFruits = false;
    private bool isRipe = false;
    private bool dataSet = false;

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

    private void Update()
    {
        Check();
    }

    public void LoadFromData(PlantData Data, PlantPreset Preset)
    {
        GardenNumber = Data.GardenNumber;
        PlantNumber = Data.SlotNumber;

        preset = Preset;

        ChangeSprite(Data.PlantLevel);
        SetFruits(preset.FruitGrowthTime);
        SetExtras();
        GrowFruit(Data.FruitsCounter);
        BugAppearance(Data.BugIsThere);

        dataSet = true;
    }

    public void SetData(PlantPreset Preset, PlantData Data) 
    {
        GardenNumber = Data.GardenNumber;
        PlantNumber = Data.SlotNumber;

        preset = Preset;

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
        SetFruits(preset.FruitGrowthTime);
        SetExtras();

        dataSet = true;
    }

    public void Check() 
    {
        if (dataSet)
        {
            if (GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].PlantLevel != plantLevel)
            {
                ChangeSprite(GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].PlantLevel);
            }
            if (!bugIsThere)
            {
                BugAppearance(GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].BugIsThere);
            }
            if (GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].FruitsCounter != fruitAmount)
            {
                GrowFruit(GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].FruitsCounter);
            }
        }
    }


    public void ChangeSprite(Level PlantLevel) 
    {
        //Debug.Log(GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].SlotNumber.ToString());
        plantLevel = PlantLevel;

        switch (PlantLevel)
        {
            case Level.LEVEL_0: 
                break;
            case Level.LEVEL_1:
                PlantDisplay.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[0].UsedSprite;
                PlantDisplay.transform.localPosition = preset.PlantObjects[0].Position;
                PlantDisplay.transform.localScale = preset.PlantObjects[0].Scale;
                gameObject.GetComponent<BoxCollider>().size = preset.PlantObjects[0].ColliderSize;
                break;
            case Level.LEVEL_2:
                PlantDisplay.gameObject.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[1].UsedSprite;
                PlantDisplay.transform.localPosition = preset.PlantObjects[1].Position;
                PlantDisplay.transform.localScale = preset.PlantObjects[1].Scale;
                gameObject.GetComponent<BoxCollider>().size = preset.PlantObjects[1].ColliderSize;
                break;
            case Level.LEVEL_3:
                PlantDisplay.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[2].UsedSprite;
                PlantDisplay.transform.localPosition = preset.PlantObjects[2].Position;
                PlantDisplay.transform.localScale = preset.PlantObjects[2].Scale;
                gameObject.GetComponent<BoxCollider>().size = preset.PlantObjects[2].ColliderSize;
                isRipe = true;
                break;
            default:
                Debug.Log("Switch: no case");
                break;
        }
    }

    public void SetFruits(float FruitTimer) 
    {
        if (preset.FruitObjects.Count == 0)
        {
            hasFruits = false;
        }
        else 
        {
            transform.GetComponent<Collider>().enabled = false;
            int fruitCount = 0;
            hasFruits = true;
            foreach (var fruitPreset in preset.FruitObjects)
            {
                GameObject fruit = Instantiate(FruitPrefab, transform.position + fruitPreset.Position, Quaternion.identity, transform);
                fruit.transform.localScale = fruitPreset.Scale;
                fruit.GetComponent<BoxCollider>().size = fruitPreset.ColliderSize;
                Fruits.Add(fruit);
                fruit.GetComponent<SpriteRenderer>().sprite = fruitPreset.RipeSprite;
                fruit.gameObject.SetActive(false);
                fruitCount++;
            }
        }      
    }

    public void SetExtras()
    {
        foreach (var extraPreset in preset.ExtraObjects)
        {
            GameObject extra = Instantiate(ExtraPrefab, transform.position + extraPreset.Position, Quaternion.identity, transform);
            extra.transform.localScale = extraPreset.Scale;
            extra.GetComponent<SpriteRenderer>().sprite = extraPreset.UsedSprite;
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
        bugIsThere = false;
        if (!hasFruits)
        {
            transform.GetComponent<Collider>().enabled = true;
        }
    }

    public void FruitHarvested()
    {
        GameplayController.Instance.FruitHarvested(GardenNumber, PlantNumber);
        --fruitAmount;
    }

    public void BugAppearance(bool BugIsThere)
    {
            Bug.SetActive(BugIsThere);
            bugIsThere = BugIsThere;
            transform.GetComponent<Collider>().enabled = !BugIsThere;
    }

    public void GrowFruit(int Amount)
    {
        int amount = Amount;
        fruitAmount = Amount;
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
        Debug.Log("hi");
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
