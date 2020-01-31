using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtons : MonoBehaviour
{
    public GameObject Buttons;

    private bool isActive;

    public void SetButtonsActive() 
    {
        isActive = !isActive;
        Buttons.SetActive(isActive);
    }
}
