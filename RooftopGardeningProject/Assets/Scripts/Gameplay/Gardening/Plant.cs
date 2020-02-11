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
    public GameObject FruitPrefab;
    public GameObject ExtraPrefab;

    public GameObject PlantSeedVfx;
    public GameObject WateringVfx;

    public GameObject VfxPrefab;

    public GameObject Bug;
    public GameObject PlantDisplay;
    public GameObject Sign;

    public Sprite WaterSignSprite;
    public Sprite GrowSignSprite;
    public Sprite FruitSignSprite;

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
    private bool isHerb = false;
    private bool wasCut = false;

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
        CheckSign();
    }

    public void LoadFromData(PlantData Data, PlantPreset Preset)
    {
        GardenNumber = Data.GardenNumber;
        PlantNumber = Data.SlotNumber;
        plantLevel = Data.PlantLevel;
        bugIsThere = Data.BugIsThere;

        preset = Preset;

        Sign.SetActive(true);
        Sign.GetComponent<SpriteRenderer>().sprite = GrowSignSprite;

        ChangeSprite(Data.PlantLevel);
        SetFruits(preset.FruitGrowthTime);
        SetExtras();
        GrowFruit(Data.FruitsCounter);
        BugAppearance(Data.BugIsThere);
        isHerb = preset.IsHerb;
        wasCut = Data.WasCut;

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
        isHerb = preset.IsHerb;

        Data.PlantLevel = Level.LEVEL_1;
        Data.MaxPlantLevel = preset.MaxLevel;
        Data.FruitsCounter = 0;
        Data.Type = Preset.Type;
        Data.GrowCycleTime = Preset.GrowCycleTime;
        Data.MaxFruitCounter = Preset.MaxFruitCounter;
        Data.CycleDisappearCount = Preset.CycleDisappearCount;
        Data.NextBugDate = Preset.StartBugDate;
        Data.NextGrowthDate = Preset.StartGrowthDate;
        Data.NextWaterDate = Preset.StartWaterDate;

        Data.SlotUsed = true;

        ChangeSprite(Data.PlantLevel);
        SetFruits(preset.FruitGrowthTime);
        SetExtras();

        dataSet = true;
        PlantSeedVfx.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void Check() 
    {
        if (dataSet)
        {
            if (GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].PlantLevel != plantLevel)
            {
                ChangeSprite(GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].PlantLevel);
                Sign.SetActive(true);
                Sign.GetComponent<SpriteRenderer>().sprite = WaterSignSprite;
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

    public void CheckSign() 
    {
        if (fruitAmount > 0 && !bugIsThere)
        {
            Sign.SetActive(true);
            Sign.GetComponent<SpriteRenderer>().sprite = FruitSignSprite;
        }
        else if (plantLevel == GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].MaxPlantLevel)
        {
            Sign.SetActive(false);
        }
    }


    public void ChangeSprite(Level PlantLevel) 
    {
        //Debug.Log(GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].SlotNumber.ToString());
        plantLevel = PlantLevel;

        if (GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].MaxPlantLevel == PlantLevel) 
        {
            isRipe = true; 
        }

        switch (PlantLevel)
        {
            case Level.LEVEL_0: 
                break;
            case Level.LEVEL_1:
                if (wasCut)
                {
                    PlantDisplay.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[3].UsedSprite;
                    PlantDisplay.transform.localPosition = preset.PlantObjects[3].Position;
                    PlantDisplay.transform.localScale = preset.PlantObjects[3].Scale;
                    gameObject.GetComponent<BoxCollider>().size = preset.PlantObjects[3].ColliderSize;
                }
                else 
                {
                    PlantDisplay.GetComponent<SpriteRenderer>().sprite = preset.PlantObjects[0].UsedSprite;
                    PlantDisplay.transform.localPosition = preset.PlantObjects[0].Position;
                    PlantDisplay.transform.localScale = preset.PlantObjects[0].Scale;
                    gameObject.GetComponent<BoxCollider>().size = preset.PlantObjects[0].ColliderSize;
                }
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
                GameObject fruit = Instantiate(FruitPrefab, transform.position /* + fruitPreset.Position */, transform.rotation, transform);
                fruit.transform.localPosition = fruitPreset.Position;
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
            GameObject extra = Instantiate(ExtraPrefab, transform.position /* + extraPreset.Position */, transform.rotation, transform);
            extra.transform.localPosition = extraPreset.Position;
            extra.transform.localScale = extraPreset.Scale;
            extra.GetComponent<SpriteRenderer>().sprite = extraPreset.UsedSprite;
        }      
    }

    public void WaterPlant()
    {
        GameplayController.Instance.WaterPlant(GardenNumber, PlantNumber, WaterCycleTime);
        if (Sign.activeSelf)
        {
            Sign.GetComponent<SpriteRenderer>().sprite = GrowSignSprite; 
        }
        WateringVfx.GetComponentInChildren<ParticleSystem>().Play();
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
        GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].CycleDisappearCount--;
        if (GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].CycleDisappearCount <= 0)
        {
            GetComponentInParent<GardenSlot>().ResetPlantSlot();
        }
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
        if (!isHerb)
        {
            if (!hasFruits && isRipe)
            {
                GameplayController.Instance.FruitHarvested(GardenNumber, PlantNumber);
                GetComponentInParent<GardenSlot>().ResetPlantSlot();
            }
        }
    }

    public void CutHerb() 
    {
        if (isHerb && isRipe)
        {
            wasCut = true;
            GameplayController.Instance.FruitHarvested(GardenNumber, PlantNumber);
            GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].CycleDisappearCount--;
            if (GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].CycleDisappearCount <= 0)
            {
                GetComponentInParent<GardenSlot>().ResetPlantSlot();
                return;
            }
            GameplayController.Instance.PlantDatas[GardenNumber][PlantNumber].PlantLevel = Level.LEVEL_1;
            ChangeSprite(Level.LEVEL_1);
            
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
