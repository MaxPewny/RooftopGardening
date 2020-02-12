using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurn : MonoBehaviour
{
    public void TurnPage() 
    {
        GetComponent<Animator>().SetTrigger("turn");
    }
}
