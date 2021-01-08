using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateChasing : State
{
    public StateChasing(Bird connectedBird) : base(connectedBird)
    {
    }

    public override Type UpdateState()
    {
        ChaseBee();
        if(bird.isEating == true)
            return typeof(StateEating);
        if(bird.isChasing)
            return typeof(StateChasing);
        else
            return typeof(StateFlying);
    }

    private void ChaseBee(){
        bird.myRb.velocity *= 0;
        if(SpawnHive.activeBees[bird.targetId] != null)
        {
            bird.transform.position = Vector2.MoveTowards(bird.transform.position, SpawnHive.activeBees[bird.targetId].transform.position, bird.speed*Time.deltaTime);   
        }
        else{
            bird.isChasing = false;
        }
    }

}
