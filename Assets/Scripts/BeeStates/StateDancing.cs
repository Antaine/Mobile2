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
    //Updates Score & Empties Capacity
    public override Type UpdateState()
    {
        UpdateScore.score += bee.honey;
        bee.honey =0;
        bee.atCapacity = false;
        return typeof(StateAtHive);
    }

}
