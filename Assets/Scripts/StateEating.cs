using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateEating : State
{
    public StateEating(Bird connectedBird) : base(connectedBird)
    {
    }
    
    
    public override Type UpdateState()
    {
        bird.isChasing = false;
        bird.isEating = false;
        return typeof(StateFlying);
    }

}
