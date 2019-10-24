using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Water : MonoBehaviour
{
    public GameObject WaterCan;
    public GameObject UsedUICanvas;

    public void SpawnWaterCan() 
    {
        Instantiate(WaterCan, transform.position, Quaternion.identity,UsedUICanvas.transform);
    }
}
