using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateDancing : State
{
    public StateDancing(Bee connectedBee) : base(connectedBee)
    {
    }

    public override Type UpdateState()
    {
        SpawnHive.score += bee.honey;
        bee.honey =0;
        Debug.Log("Score "+ SpawnHive.score);
        bee.atCapacity = false;
        return typeof(StateAtHive);
    }

}
