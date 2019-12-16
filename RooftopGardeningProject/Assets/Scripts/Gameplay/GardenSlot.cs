using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenSlot : MonoBehaviour 
{ 
    public bool SlotUsed = false;
    public int SlotNumber;
    private Garden currentGarden;

    public List<PlantPreset> PlantPresets;
    public GameObject Plant;

    // Start is called before the first frame update
    private void Start()
    {
        currentGarden = GetComponentInParent<Garden>();
        if (GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].SlotUsed == true)
        {
            Plant = Instantiate(currentGarden.PlantPrefab, transform.position, transform.rotation, transform);
            foreach (PlantPreset preset in PlantPresets)
            {
                if (GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].Type == preset.Type)
                {
                    Plant.GetComponent<Plant>().LoadFromData(GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber], preset);
                }
            } 
            SlotUsed = true;
        }
    }

    public void PlantSeed(PlantType SelectedType) 
    {
        if (GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].SlotUsed == false && SlotUsed == false)
        {
            Plant = Instantiate(currentGarden.PlantPrefab, transform.position, transform.rotation, transform);
            SlotUsed = true;
            foreach (PlantPreset preset in PlantPresets)
            {
                if (preset.Type == SelectedType)
                {
                    Plant.GetComponent<Plant>().SetData(preset, GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber]);
                    break;
                }
            }
        }
    }

    public void OpenSeedMenu() 
    {
        if (SlotUsed == false)
        {
            currentGarden.OpenPlantMenu(this);
        }
    }

    public void ResetPlantSlot()
    {
        SlotUsed = false;
        GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].SlotUsed = false;
        Destroy(Plant);
    }

    public void DebugGrowPlant() 
    {
        if (SlotUsed == true)
        {
            if (GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].PlantLevel < Level.LEVEL_3)
            {
                GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].PlantLevel++;
            }  
            Plant.GetComponent<Plant>().ChangeSprite(GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].PlantLevel);
        }
    }

    public void DebugGrowFruit()
    {
        if (SlotUsed == true)
        {
            if (GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].FruitsCounter < GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].MaxFruitCounter)
            {
                GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].FruitsCounter++;
            }
            Plant.GetComponent<Plant>().GrowFruit(GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].FruitsCounter);
        }
    }

    public void DebugBugAppear()
    {
        if (SlotUsed == true)
        {
            GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].BugIsThere = true;
            
            Plant.GetComponent<Plant>().BugAppearance(GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].BugIsThere);
        }
    }
}


