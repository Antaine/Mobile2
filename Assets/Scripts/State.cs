using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class State
{
    protected Bee bee;
    protected Rigidbody2D myRb;
    protected CapsuleCollider2D BeeCollider;
    protected SpriteRenderer sprite;
        

    public State(Bee connectedBee)
    {
        bee = connectedBee;
    }
    public virtual void EnterState()
    {

    }

    public virtual Type UpdateState()
    {
        return typeof(State);
    }

    public virtual void ExitState()
    {

    }
}
