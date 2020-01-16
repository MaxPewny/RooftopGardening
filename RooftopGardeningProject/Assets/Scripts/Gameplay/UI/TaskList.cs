using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    public Neighbor SelectedNeighbor;

    public Image XpFill;
    public Text LevelDisplay;
    public Text Name;

    public GameObject NeighborList;
    public GameObject TaskCanvas;
    public GameObject TaskPrefab;

    private float xp;
    private int level;
    private List<GameObject> tasks;

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
        task.GetComponent<TaskObject>().SetPreset(SetTaskPreset, this);
        tasks.Add(task);
    }

    public void SwitchBack() 
    {
        foreach (GameObject task in tasks)
        {
            Destroy(task);
        }
        tasks.Clear();
        NeighborList.SetActive(true);
        gameObject.SetActive(false);
    }
}