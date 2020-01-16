using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskObject : MonoBehaviour
{
    public Text NameDisplay;
    public Text FirstObjectiveText;
    public Text SecondObjectiveText;

    public Image FirstIcon;
    public Image SecondIcon;

    private NeighborTaskPreset preset;
    private TaskList taskList;

    public void SetPreset(NeighborTaskPreset Preset, TaskList TaskList)
    {
        preset = Preset;
        taskList = TaskList;
        NameDisplay.text = preset.Name;
        SetUI(ref FirstIcon, ref FirstObjectiveText, 0);
        SetUI(ref SecondIcon, ref SecondObjectiveText, 1);
    }

    private void SetUI(ref Image Icon, ref Text ObjectiveText, int I) 
    {
        Icon.sprite = preset.Objectives[I].Icon;

        foreach (PlantCurrency currency in GameplayController.Instance.PlantCurrencies)
        {
            if (currency.Plant == preset.Objectives[I].Type)
            {
                ObjectiveText.text = currency.Fruit + " / " + preset.Objectives[I].Count;
                return;
            }
        }
    }

    public void CompleteTask() 
    {
        foreach (NeighborTaskPreset.ObjectivePair objective in preset.Objectives)
        {
            foreach (PlantCurrency currency in GameplayController.Instance.PlantCurrencies)
            {
                if (currency.Plant == objective.Type)
                {
                    if (currency.Fruit < objective.Count)
                    {
                        return;
                    }
                }
            }
        }

        foreach (NeighborTaskPreset.ObjectivePair objective in preset.Objectives)
        {
            foreach (PlantCurrency currency in GameplayController.Instance.PlantCurrencies)
            {
                if (currency.Plant == objective.Type)
                {
                    currency.Fruit -= objective.Count;
                }
            }
        }
        GrantReward();
        taskList.SwitchBack();
    }

    public void GrantReward() 
    {
        foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
        {
            if (data.NeighborEnum == preset.TaskGiver)
            {
                data.NeighborXp += preset.RewardedXP;
                data.SolvedTasks++;
            }
        }
    }
}
