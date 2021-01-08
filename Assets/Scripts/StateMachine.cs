using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    private State currentState;
    private Dictionary<Type, State> allStates;

    public void SetUpStates(Dictionary<Type, State> states,Type defaultState)
    {
        allStates = states;
        currentState = allStates[defaultState];
    }

    private void Update()
    {
        if(currentState == null) return;
        Type nextState = currentState.UpdateState();
        if(nextState != currentState.GetType())
            SwitchState(allStates[nextState]);
    }
    private void SwitchState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
