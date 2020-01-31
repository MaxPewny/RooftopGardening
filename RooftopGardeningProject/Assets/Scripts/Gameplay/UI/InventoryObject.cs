using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    public Image IconDisplay;
    public Text NameDisplay;
    public Text CountDisplay;

    private InventoryPreset preset;
    private InventoryList inventoryList;


    public void SetPreset(InventoryPreset Preset, InventoryList AssignedList, uint FruitCount)
    {
        preset = Preset;
        inventoryList = AssignedList;

        IconDisplay.sprite = preset.Icon;
        NameDisplay.text = preset.Name;
        CountDisplay.text = "x" + FruitCount.ToString();
    }
}
