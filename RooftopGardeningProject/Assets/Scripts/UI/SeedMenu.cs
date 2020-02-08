using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMenu : MonoBehaviour
{
    

    public List<ScriptableObject> SeedPackPresets;

    public List<GameObject> SeedBags = new List<GameObject>();

    public GameObject SeedBagPrefab;

    public GameObject ScrollContent;

    public GameObject ClickDetect;

    private GardenSlot currentSlot;

    private void ShowSeeds() 
    {
        //foreach (PlantCurrency Currency in GameplayController.Instance.PlantCurrencies)
        //{
        //    if (Currency.Seed > 0)
        //    {
        //        GameObject seedBag = Instantiate(SeedBagPrefab, ScrollContent.transform);
        //        SeedBags.Add(seedBag);
        //        foreach (SeedPackPreset pack in SeedPackPresets)
        //        {
        //            if (Currency.Plant == pack.Type)
        //            {
        //                seedBag.GetComponent<SeedUi>().SetValues(pack, this);
        //                break;
        //            }
        //        }
        //    }
        //}
        foreach (SeedPackPreset pack in SeedPackPresets)
        {
            foreach (PlantCurrency Currency in GameplayController.Instance.PlantCurrencies)
            {
                if (Currency.Seed > 0f && Currency.Plant == pack.Type)
                {
                    GameObject seedBag = Instantiate(SeedBagPrefab, ScrollContent.transform);
                    SeedBags.Add(seedBag);
                    seedBag.GetComponent<SeedUi>().SetValues(pack, this);
                    break;
                }
            }
        }
    }

    public void SetSlot(GardenSlot Slot) 
    {
        currentSlot = Slot;
    }

    public void PlantSeed(PlantType Type)
    {
        currentSlot.PlantSeed(Type);
        foreach (PlantCurrency currency in GameplayController.Instance.PlantCurrencies)
        {
            if (currency.Plant == Type)
            {
                currency.Seed--;
                break;
            }
        }
        
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ShowSeeds();
        ClickDetect.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (GameObject bag in SeedBags)
        {
            Destroy(bag);
        }
        SeedBags.Clear();
        currentSlot = null;
        ClickDetect.SetActive(false);
    }
}
