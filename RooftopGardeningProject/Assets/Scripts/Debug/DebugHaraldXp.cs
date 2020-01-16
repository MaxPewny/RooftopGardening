using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugHaraldXp : MonoBehaviour
{
    public Text xpText;

    // Update is called once per frame
    void Update()
    {
        foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
        {
            if (data.NeighborEnum == Neighbor.HARALD)
            {
                xpText.text = data.NeighborXp.ToString();
            }
        }
    }
}
