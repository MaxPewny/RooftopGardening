using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject NeighborList;
    public GameObject TaskList;
    public GameObject RecipeList;
    public GameObject InventoryList;

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
    public void ShowRecipes() 
    {
        activeMenu.SetActive(false);
        RecipeList.SetActive(true);
        SetActiveMenu(RecipeList);
    }
    public void ShowInventory()
    {
        activeMenu.SetActive(false);
        InventoryList.SetActive(true);
        SetActiveMenu(InventoryList);
    }

    public void SetActiveMenu(GameObject ActiveMenu) 
    {
        activeMenu = ActiveMenu;
    }
}
