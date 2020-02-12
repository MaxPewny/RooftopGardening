using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborList : MonoBehaviour
{
    public GameObject NeighborPrefab;
    public GameObject ListCanvas;
    public GameObject PageTurn;

    public GameObject TaskList;
    public MenuManager UsedManager;

    private void Awake()
    {
        foreach (Neighbor neighbor in (Neighbor[])Enum.GetValues(typeof(Neighbor)))
        {
            if ((int)neighbor < (int)GameplayController.Instance.PlayerLevel)
            {
                GameObject neighborObject = Instantiate(NeighborPrefab, ListCanvas.transform);
                neighborObject.GetComponent<NeighborObject>().SetValues(neighbor, gameObject, TaskList, UsedManager);
            }
        }
    }

    public void ShowHaraldTasks() 
    {
        foreach (NeighborObject neighbor in GetComponentsInChildren<NeighborObject>())
        {
            if (neighbor.SelectedNeighbor == Neighbor.HARALD)
            {
                neighbor.ShowTaskList();
            }
        }
    }
    public void ShowMiraTasks()
    {
        foreach (NeighborObject neighbor in GetComponentsInChildren<NeighborObject>())
        {
            if (neighbor.SelectedNeighbor == Neighbor.MIRA)
            {
                neighbor.ShowTaskList();
            }
        }
    }
    public void ShowKitaTasks()
    {
        foreach (NeighborObject neighbor in GetComponentsInChildren<NeighborObject>())
        {
            if (neighbor.SelectedNeighbor == Neighbor.KITA)
            {
                neighbor.ShowTaskList();
            }
        }
    }
    private void OnDisable()
    {
        PageTurn.GetComponent<PageTurn>().TurnPage();
    }
}
