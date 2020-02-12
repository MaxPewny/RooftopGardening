using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSetLevel : MonoBehaviour
{
    public void LevelUp() 
    {
        GameplayController.Instance.PlayerLevel = Level.LEVEL_10;
    }
}
