using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    public GameObject PlantPrefab;

    public GameObject EmptySlotPrefab;

    public GameObject PlantMenuPrefab;

    public Canvas UsedCanvas;

    private void Start()
    {

    }

    public void OpenPlantMenu(GardenSlot SelectedSlot) 
    {
        Instantiate(PlantMenuPrefab, UsedCanvas.transform);
    }
}
