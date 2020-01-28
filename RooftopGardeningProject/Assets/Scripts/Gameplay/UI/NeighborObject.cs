using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborObject : MonoBehaviour
{
    public Neighbor SelectedNeighbor;
    
    public GameObject NeighborList;
    public GameObject NeighborTaskList;

    public MenuManager Manager;

    public List<NeighborTaskPreset> TaskPresets;

    public Image XpFill;
    public Text LevelDisplay;
    public Text Name;
    public Text RemainingTasksText;
    public Text ReadyTasksText;

    private float xpFillValue;
    private int level;
    private int remainingTasks;
    private int maxRemainingTasks;
    private int readyTasks;

    public void SetValues(Neighbor UsedNeighbor, GameObject UsedNeighborList, GameObject UsedTaskList, MenuManager AssignedManager )
    {
        SelectedNeighbor = UsedNeighbor;
        NeighborList = UsedNeighborList;
        NeighborTaskList = UsedTaskList;
        Manager = AssignedManager;

        for (int i = TaskPresets.Count - 1; i >= 0; i--)
        {
            if (TaskPresets[i].TaskGiver != SelectedNeighbor)
            {
                TaskPresets.RemoveAt(i);
            }
        }

        NeighborData usedData;

        foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
        {
            if (data.NeighborEnum == SelectedNeighbor)
            {
                usedData = data;

                xpFillValue = usedData.NeighborXp / usedData.MaxXp;
                level = (int)usedData.NeighborLevel;

                LevelDisplay.text = level.ToString();
                XpFill.fillAmount = xpFillValue;
                Name.text = SelectedNeighbor.ToString();
                RemainingTasksText.text = remainingTasks.ToString();

                maxRemainingTasks = TaskPresets.Count;
                remainingTasks = maxRemainingTasks - usedData.SolvedTasks;
                if (remainingTasks < 0)
                {
                    remainingTasks = 0;
                }
                //Debug.Log("xFill " + xpFillValue);
                //Debug.Log("xneigh " + usedData.NeighborXp);
                //Debug.Log("xmax " + usedData.MaxXp);

                break;
            }
        }
    }


    private void OnEnable()
    {
        NeighborData usedData;

        foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
        {
            if (data.NeighborEnum == SelectedNeighbor)
            {
                usedData = data;

                xpFillValue = usedData.NeighborXp / usedData.MaxXp;
                level = (int)usedData.NeighborLevel;

                LevelDisplay.text = level.ToString();
                XpFill.fillAmount = xpFillValue;
                Name.text = SelectedNeighbor.ToString();
                RemainingTasksText.text = remainingTasks.ToString();

                maxRemainingTasks = TaskPresets.Count;
                remainingTasks = maxRemainingTasks - usedData.SolvedTasks;
                if (remainingTasks < 0)
                {
                    remainingTasks = 0;
                }
                //Debug.Log("xFill " + xpFillValue);
                //Debug.Log("xneigh " + usedData.NeighborXp);
                //Debug.Log("xmax " + usedData.MaxXp);

                break;
            }
        }

        if (remainingTasks == 0)
        {
            ReadyTasksText.text = "0";
            return;
        }

        foreach (NeighborTaskPreset.ObjectivePair objective in TaskPresets[maxRemainingTasks - remainingTasks].Objectives)
        {
            foreach (PlantCurrency currency in GameplayController.Instance.PlantCurrencies)
            {
                if (currency.Plant == objective.Type)
                {
                    if (currency.Fruit < objective.Count)
                    {
                        ReadyTasksText.text = "0";
                        return;
                    }
                }
            }
        }

        readyTasks++;
        ReadyTasksText.text = readyTasks.ToString();
    }


    private void Update()
    {
        foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
        {
            if (data.NeighborEnum == SelectedNeighbor)
            {
                xpFillValue = data.NeighborXp / data.MaxXp;
                level = (int)data.NeighborLevel;

                LevelDisplay.text = level.ToString();
                XpFill.fillAmount = xpFillValue;
                return;
            }
        }
    }

    public void ShowTaskList() 
    {
        NeighborTaskList.SetActive(true);
        Manager.SetActiveMenu(NeighborTaskList);
        //Debug.Log(maxRemainingTasks - remainingTasks);
        if (maxRemainingTasks-remainingTasks >= maxRemainingTasks)
        {
            NeighborTaskList.GetComponent<TaskList>().SetUI(SelectedNeighbor, TaskPresets[0], xpFillValue, level);
        }
        else
        {
            NeighborTaskList.GetComponent<TaskList>().SetUI(SelectedNeighbor, TaskPresets[maxRemainingTasks - remainingTasks], xpFillValue, level);
        }
        NeighborList.SetActive(false);
    }
}
