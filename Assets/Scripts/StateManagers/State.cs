﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Generic State
public abstract class State
{
    protected Bee bee;
    protected Bird bird;
    protected Rigidbody2D myRb;
    protected CapsuleCollider2D BeeCollider;
    protected SpriteRenderer sprite;
        
    public State(Bee connectedBee)
    {
        bee = connectedBee;
    }

    public State(Bird connectedBird)
    {
        bird = connectedBird;
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
