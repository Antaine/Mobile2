using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateFlying : State
{
    public StateFlying(Bird connectedBird) : base(connectedBird)
    {
    }
}
