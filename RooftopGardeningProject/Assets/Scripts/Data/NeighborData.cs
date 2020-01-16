﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborData 
{
    public Level NeighborLevel;
    public Neighbor NeighborEnum;

    public float NeighborXp;

    public int SolvedTasks;

    public NeighborData(Neighbor neighbor) 
    {
        NeighborEnum = neighbor;
    }    
}
