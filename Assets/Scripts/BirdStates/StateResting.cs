using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateResting : State
{
    public StateResting(Bird connectedBird) : base(connectedBird)
    {
    }

    public override Type UpdateState()
    {
        bird.isFleeing = false;
        Rest();
        bird.CheckEnergy(bird);
        if(!bird.isResting){
            return typeof(StateFlying);
        }
        return typeof(StateResting);
    }

    private void Rest(){
        bird.myRb.velocity = new Vector2(0,0);
        bird.energyRate = 0.3f;
        bird.isResting = true;
        if(bird.currEnergy>=bird.maxEnergy){
            bird.isResting = false;
            bird.atNest = false;
        }
    }
}
