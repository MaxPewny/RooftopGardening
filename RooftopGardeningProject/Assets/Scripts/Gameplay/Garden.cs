using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    public int GardenNumber;

    public GameObject PlantPrefab;

    public GameObject EmptySlotPrefab;

    public GameObject PlantMenu;

    public float WeedAppearanceTimer;

    public List<GameObject> Weeds;

    private void Start()
    {
        GameplayController.Instance.GardenDatas[GardenNumber].WeedAppearanceTimer = WeedAppearanceTimer;

        foreach (Weed weed in GetComponentsInChildren<Weed>())
        {
            Weeds.Add(weed.gameObject);
            weed.gameObject.SetActive(false);
        }
        GameplayController.Instance.GardenDatas[GardenNumber].MaxWeedCounter = Weeds.Count;
    }

    public void Check()
    {
        GardenData data = GameplayController.Instance.GardenDatas[GardenNumber];
        GrowWeed(data.WeedCounter);

    }

    public void OpenPlantMenu(GardenSlot SelectedSlot) 
    {
        PlantMenu.SetActive(true);
        PlantMenu.GetComponent<SeedMenu>().SetSlot(SelectedSlot);
        //PlantMenu.GetComponent<SeedMenu>().ShowSeeds();
    }

    public void GrowWeed(int Amount)
    {
        int amount = Amount;
        {
            for (int i = 0; i < Weeds.Count; i++)
            {
                if (Weeds[i].activeSelf == true)
                {
                    amount--;
                }
            }
            for (int i = 0; i < Weeds.Count; i++)
            {
                if (amount <= 0)
                {
                    return;
                }
                else if (Weeds[i].activeSelf == false)
                {
                    Weeds[i].SetActive(true);
                    amount--;
                }
            }

        }

    }
}
