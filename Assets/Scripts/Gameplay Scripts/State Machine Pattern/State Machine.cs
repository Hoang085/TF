using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> states = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> currentState;

    protected bool isTransitioningState = false;
    private void Start()
    {
        currentState.EnterState();
    }
    private void Update()
    {
        EState nextStateKey = currentState.GetNextState();

        if (nextStateKey.Equals(currentState.stateKey)  && !isTransitioningState)
        {
            currentState.UpdateState();
        }
        else if(!isTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }

    private void TransitionToState(EState stateKey)
    {
        isTransitioningState = true;

        currentState.ExitState();
        currentState = states[stateKey];
        currentState.EnterState();

        isTransitioningState = false;
    }
}
