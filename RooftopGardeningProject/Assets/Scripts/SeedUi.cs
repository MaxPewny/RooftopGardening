﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeedUi : MonoBehaviour, IPointerClickHandler
{
    PlantType type;

    public Sprite Icon;

    public Text CountText;

    public Text NameText;

    SeedMenu menu;

    public void SetValues(SeedPackPreset Pack, SeedMenu seedMenu) 
    {
        type = Pack.Type;
        Icon = Pack.Seed;
        NameText.text = Pack.Name;
        menu = seedMenu;

        foreach (PlantCurrency currency in GameplayController.Instance.PlantCurrencies)
        {
            if (currency.Plant == type)
            {
                CountText.text = currency.Seed.ToString();
                return;
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        menu.PlantSeed(type);
    }
}
