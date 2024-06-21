using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState, ObjectType> where EState : Enum
{
    public abstract ObjectType stateObject { get; set; }
    public BaseState(EState key)
    {
        stateKey = key;
    }
    public EState stateKey { get; private set; }

    public abstract void EnterState(StateMachine<EState, ObjectType> stateMachine, ObjectType unit);
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
    public abstract void OnTriggerEnter();
    public abstract void OnTriggerStay();
    public abstract void OnTriggerExit();
}
