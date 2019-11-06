﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bug : MonoBehaviour, IPointerClickHandler
{
    public int RemoveTapAmount = 10;
    private int removeTapCount = 0;

    private void Start()
    {
        removeTapCount = RemoveTapAmount;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        removeTapCount--;
        if (removeTapCount <= 0) Destroy(this.gameObject);
    }
}
