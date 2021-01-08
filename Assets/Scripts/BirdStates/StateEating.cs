using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateEating : State
{

    public StateEating(Bird connectedBird) : base(connectedBird)
    {
    }
    
    //Eat Bee
    public override Type UpdateState()
    {
        bird.isChasing = false;
        bird.isEating = false;
        bird.currEnergy = bird.maxEnergy;
        return typeof(StateFlying);
    }

}
