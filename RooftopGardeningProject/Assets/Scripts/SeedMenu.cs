using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMenu : MonoBehaviour
{
    public GameObject SeedBagPrefab;

    private GameObject currentSlot;

    public List<ScriptableObject> SeedPackPresets;

    public GameObject ScrollContent;

    public void ShowSeeds() 
    {
        foreach (PlantCurrency Currency in GameplayController.Instance.PlantCurrencies)
        {
            if (Currency.Seed > 0)
            {
                GameObject seedBag = Instantiate(SeedBagPrefab, ScrollContent.transform);
                foreach (SeedPackPreset pack in SeedPackPresets)
                {
                    if (Currency.Plant == pack.Type)
                    {
                        seedBag.GetComponent<SeedUi>().SetValues(pack, this);
                        break;
                    }
                }
            }
        }
    }

    public void SetSlot(GameObject Slot) 
    {
        currentSlot = Slot;
    }

    public void PlantSeed(PlantType Type)
    {
        currentSlot.GetComponent<GardenSlot>().PlantSeed(Type);
        currentSlot = null;
        gameObject.SetActive(false);
    }
}
