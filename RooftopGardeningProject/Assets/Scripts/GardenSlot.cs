using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GardenSlot : MonoBehaviour, IPointerClickHandler
{

    public int SlotNumber;
    private Garden currentGarden;

    public List<ScriptableObject> PlantPresets;
    public GameObject Plant;

    // Start is called before the first frame update
    private void Start()
    {
        currentGarden = GetComponentInParent<Garden>();
        if (GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].SlotUsed == true)
        {
            Plant = Instantiate(currentGarden.PlantPrefab, transform);
            Plant.GetComponent<Plant>().LoadFromData(GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber]);
        }
    }

    public void PlantSeed(PlantType SelectedType) 
    {
        if (GameplayController.Instance.PlantDatas[currentGarden.GardenNumber][SlotNumber].SlotUsed == false)
        {
            Plant = Instantiate(currentGarden.PlantPrefab, transform);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}


