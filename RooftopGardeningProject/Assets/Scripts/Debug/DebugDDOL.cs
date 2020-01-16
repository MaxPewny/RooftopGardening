using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDDOL : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
