using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CurrencyScriptableObject", order = 1)]
public class Currency : ScriptableObject
{
    public float SoftCurrency;
    public float HardCurrency;
}
