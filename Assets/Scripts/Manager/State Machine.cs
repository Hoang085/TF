using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<EState, ObjectType> : MonoBehaviour where EState : Enum
{
    [SerializeField] internal ObjectType stateObject;

    protected Dictionary<EState, BaseState<EState, ObjectType>> states = new Dictionary<EState, BaseState<EState, ObjectType>>();
    protected BaseState<EState, ObjectType> currentState;

    protected bool isTransitioningState = false;
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

    internal void TransitionToState(EState stateKey)
    {
        isTransitioningState = true;

        currentState?.ExitState();
        currentState = states[stateKey];
        currentState.EnterState(this, stateObject);

        isTransitioningState = false;
    }
}
