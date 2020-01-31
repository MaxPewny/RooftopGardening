using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    public GameObject InventoryPrefab;
    public GameObject ListCanvas;
    public List<InventoryPreset> Presets = new List<InventoryPreset>();
    private List<GameObject> listEntries = new List<GameObject>();

    private void OnEnable()
    {
        foreach (PlantCurrency currency in GameplayController.Instance.PlantCurrencies)
        {
            if (currency.Fruit > 0)
            {
                foreach (InventoryPreset preset in Presets)
                {
                    if (currency.Plant == preset.Type)
                    {
                        GameObject entry = Instantiate(InventoryPrefab, ListCanvas.transform);
                        listEntries.Add(entry);
                        entry.GetComponent<InventoryObject>().SetPreset(preset, this, currency.Fruit);
                    }
                }
            }
        }
    }

    public void OnDisable()
    {
        for (int i = listEntries.Count - 1; i >= 0; i--)
        {
            Destroy(listEntries[i]);
        }
        listEntries.Clear();
    }
}
