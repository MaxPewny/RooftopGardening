using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject NeighborList;
    public GameObject TaskList;
    //public GameObject RecepieList;

    private GameObject activeMenu;

    private void Start()
    {
        activeMenu = NeighborList;
    }

    public void ShowNeighbors() 
    {
        activeMenu.SetActive(false);
        NeighborList.SetActive(true);
        SetActiveMenu(NeighborList);
    }

    public void ShowTasks() 
    {
        activeMenu.SetActive(false);
        TaskList.SetActive(true);
        SetActiveMenu(TaskList);
    }

    public void SetActiveMenu(GameObject ActiveMenu) 
    {
        activeMenu = ActiveMenu;
    }
}
