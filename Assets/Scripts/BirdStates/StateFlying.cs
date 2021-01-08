using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateFlying : State
{
    public StateFlying(Bird connectedBird) : base(connectedBird)
    {
    }

    //Manages Flying
    public override Type UpdateState()
    {
        bird.energyRate = -0.4f;
        bird.CheckEnergy(bird);
        if(bird.atNest)
            return typeof(StateResting);
        if(bird.isFleeing){
            bird.ReturnToNest(bird);
            return typeof(StateFlying);
        }
        bird.myRb.velocity = bird.birdDir*bird.speed;
        bird.ScanBees(bird);
        if(bird.isChasing){
            Debug.Log("Found bee");
            return typeof(StateChasing);
        }
        return typeof(StateFlying);
    }
}
