using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    public MenuManager Manager;
    public Neighbor SelectedNeighbor;

    public Image XpFill;
    public Text LevelDisplay;
    public Text Name;

    public GameObject NeighborList;
    public GameObject TaskCanvas;
    public GameObject TaskPrefab;

    private float xp;
    private int level;
    private List<GameObject> tasks = new List<GameObject>();

    public void SetUI(Neighbor SetNeighbor, NeighborTaskPreset SetTaskPreset, float SetXp, int SetLevel) 
    {
        SelectedNeighbor = SetNeighbor;
        xp = SetXp;
        level = SetLevel;

        XpFill.fillAmount = xp;
        LevelDisplay.text = level.ToString();
        Name.text = SelectedNeighbor.ToString();

        SetTasks(SetTaskPreset);
    }

    public void SetTasks(NeighborTaskPreset SetTaskPreset)
    {
        GameObject task = Instantiate(TaskPrefab,TaskCanvas.transform);
        tasks.Add(task);
        task.GetComponent<TaskObject>().SetPreset(SetTaskPreset, this);
    }

    public void SwitchBack() 
    {
        Manager.SetActiveMenu(NeighborList);
        
        NeighborList.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        for (int i = tasks.Count - 1; i >= 0; i--)
        {
            Destroy(tasks[i]);
        }
        tasks.Clear();
    }
}