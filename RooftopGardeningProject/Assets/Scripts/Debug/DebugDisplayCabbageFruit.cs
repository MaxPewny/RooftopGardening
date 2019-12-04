using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugDisplayCabbageFruit : MonoBehaviour
{
    public Text DebugText;

    PlantCurrency currency;

    private void Start()
    {
        foreach (PlantCurrency item in GameplayController.Instance.PlantCurrencies)
        {
            if (item.Plant == PlantType.CABBAGE)
            {
                currency = item;
            }
        }
    }

    private void Update()
    {
        DebugText.text = "Cabbages " + currency.Fruit.ToString();
        
    }
}
