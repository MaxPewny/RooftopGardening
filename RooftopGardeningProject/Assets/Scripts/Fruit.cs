using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fruit : MonoBehaviour, IPointerClickHandler
{
    private Plant currentPlant;

    private void Start()
    {
        currentPlant = GetComponentInParent<Plant>();
        gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        currentPlant.FruitHarvested();
        gameObject.SetActive(false);
    }
    
}
