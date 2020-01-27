using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborList : MonoBehaviour
{
    public GameObject NeighborPrefab;
    public GameObject ListCanvas;

    public GameObject TaskList;
    public MenuManager UsedManager;

    private void Start()
    {
        foreach (Neighbor neighbor in (Neighbor[])Enum.GetValues(typeof(Neighbor)))
        {
            GameObject neighborObject = Instantiate(NeighborPrefab, ListCanvas.transform);
            neighborObject.GetComponent<NeighborObject>().SetValues(neighbor, gameObject, TaskList, UsedManager);
        }
    }
}
