using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSnapAfterAnim : MonoBehaviour
{
    public void SnapBack() 
    {
        GetComponentInParent<WaterCan>().SnapWaterCan();
    }
}
