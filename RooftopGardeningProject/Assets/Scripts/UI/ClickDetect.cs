using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetect : MonoBehaviour, IPointerClickHandler
{
    public GameObject relatedObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        relatedObject.SetActive(false);
        Debug.Log("inactive");
    }
}
