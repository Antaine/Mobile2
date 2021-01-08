using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StateGathering : State
{
    public StateGathering(Bee connectedBee) : base(connectedBee)
    {
    }

    //Manages State
    public override Type UpdateState()
    {
        bee.honey++;
        bee.atFlower = false;
        if(bee.honey>=bee.capacity){return typeof(StateFleeing);}
        else
            return typeof(StateSearching);
    }
}
