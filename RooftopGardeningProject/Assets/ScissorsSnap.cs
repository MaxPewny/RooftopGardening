using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsSnap : MonoBehaviour
{
    public void SnapBack()
    {
        GetComponentInParent<HerbScissors>().SnapScissors();
    }
}
