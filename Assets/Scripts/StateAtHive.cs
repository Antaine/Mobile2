using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateAtHive : State
{
    public StateAtHive(Bee connectedBee) : base(connectedBee)
    {
    }
    
    public override Type UpdateState()
    {
        bee.isFleeing = false;
        bee.flowerFound = false;
        Rest(bee);
        bee.CheckEnergy(bee);
        if(!bee.isRested)
            return typeof(StateAtHive);
        else
            bee.atHive = false;
            return typeof(StateSearching);
    }

    private void Rest(Bee bee){
        bee.myRb.velocity = new Vector2(0,0);
        bee.energyRate = 0.2f;
        bee.isRested = false;
        if(bee.currEnergy>=bee.maxEnergy){
            bee.isRested = true;
        }
    }
}
