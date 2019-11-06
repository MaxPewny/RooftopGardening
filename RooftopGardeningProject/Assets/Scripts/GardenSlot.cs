using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GardenSlot : MonoBehaviour, IPointerClickHandler
{
    public GardenSlotData Data;

    private Garden currentGarden;

    public GameObject Plant;

    // Start is called before the first frame update
    void Start()
    {
        currentGarden = GetComponentInParent<Garden>();
        if (Data.Plant != null)
        {
            Plant = Instantiate(currentGarden.PlantPrefab);
            Plant.GetComponent<Plant>().LoadFromData(Data.Plant);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }
}
