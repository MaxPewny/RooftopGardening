using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    public MenuManager Manager;
    public Neighbor SelectedNeighbor;

    public Image Background;
    public Image XpFill;
    public Text LevelDisplay;
    public Text Name;

    public GameObject NeighborList;
    public GameObject TaskCanvas;
    public GameObject TaskPrefab;

    public Sprite HaraldBg;
    public Sprite MiraBg;
    public Sprite KitaBg;
    public Sprite DefaultBg;

    private float xpFillValue;
    private int level;
    private List<GameObject> tasks = new List<GameObject>();

    public void SetUI(Neighbor SetNeighbor, NeighborTaskPreset SetTaskPreset, float SetXp, int SetLevel) 
    {
        SelectedNeighbor = SetNeighbor;
        xpFillValue = SetXp;
        level = SetLevel;

        XpFill.fillAmount = xpFillValue;
        LevelDisplay.text = level.ToString();
        Name.text = SelectedNeighbor.ToString();

        switch (SelectedNeighbor)
        {
            case Neighbor.HARALD:
                Background.sprite = HaraldBg;
                break;
            case Neighbor.MIRA:
                Background.sprite = MiraBg;
                break;
            case Neighbor.KITA:
                Background.sprite = KitaBg;
                break;
            default:
                Background.sprite = DefaultBg;
                break;
        }

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

    public void SwitchBackHarald()
    {
        Manager.SetActiveMenu(NeighborList);

        NeighborList.SetActive(true);
        gameObject.SetActive(false);
        NeighborList.GetComponent<NeighborList>().ShowHaraldTasks();
    }

    public void SwitchBackMira()
    {
        Manager.SetActiveMenu(NeighborList);

        NeighborList.SetActive(true);
        gameObject.SetActive(false);
        NeighborList.GetComponent<NeighborList>().ShowMiraTasks();
    }

    public void SwitchBackKita()
    {
        Manager.SetActiveMenu(NeighborList);

        NeighborList.SetActive(true);
        gameObject.SetActive(false);
        NeighborList.GetComponent<NeighborList>().ShowKitaTasks();
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