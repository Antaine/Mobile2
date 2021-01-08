using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class StateDancing : State
{
    public StateDancing(Bee connectedBee) : base(connectedBee)
    {
    }

    public override Type UpdateState()
    {
        UpdateScore.score += bee.honey;
        bee.honey =0;
        //Debug.Log("Score "+ SpawnHive.score);
        bee.atCapacity = false;
        return typeof(StateAtHive);
    }

}
