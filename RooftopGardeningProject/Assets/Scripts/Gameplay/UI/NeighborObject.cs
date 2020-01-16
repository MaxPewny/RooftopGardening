using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborObject : MonoBehaviour
{
    public MenuManager Manager;
    public Neighbor SelectedNeighbor;

    public List<NeighborTaskPreset> TaskPresets;

    public GameObject NeighborList;
    public GameObject NeighborTaskList;

    public Image XpFill;
    public Text LevelDisplay;
    public Text Name;
    public Text RemainingTasksText;
    public Text ReadyTasksText;

    private float xp;
    private int level;
    private int remainingTasks;
    private int maxRemainingTasks;
    private int readyTasks;

    private void Awake()
    {
        NeighborData usedData;

        foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
        {
            if (data.NeighborEnum == SelectedNeighbor)
            {
                usedData = data;
                maxRemainingTasks = TaskPresets.Count;
                remainingTasks = maxRemainingTasks - usedData.SolvedTasks;
                xp = usedData.NeighborXp;
                level = (int)usedData.NeighborLevel;

                LevelDisplay.text = level.ToString();
                XpFill.fillAmount = xp;
                Name.text = SelectedNeighbor.ToString();
                RemainingTasksText.text = remainingTasks.ToString();

            }
        }
    }

    private void OnEnable()
    {
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

    public void ShowTaskList() 
    {
        NeighborTaskList.SetActive(true);
        NeighborTaskList.GetComponent<TaskList>().SetUI(SelectedNeighbor, TaskPresets[maxRemainingTasks - remainingTasks], xp, level);
        NeighborList.SetActive(false);
    }
}
