using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMenu : MonoBehaviour
{
    public void ShowSeeds() 
    {
        foreach (PlantCurrency Currency in GameplayController.Instance.PlantCurrencies)
        {
            if (Currency.Seed > 0)
            {
                //Instantiate();
            }
        }
    }
}
