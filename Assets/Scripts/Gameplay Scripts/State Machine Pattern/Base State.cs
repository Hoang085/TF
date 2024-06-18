using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public BaseState(EState key)
    {
        stateKey = key;
    }
    public EState stateKey { get; private set; }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
    public abstract void OnTriggerEnter();
    public abstract void OnTriggerStay();
    public abstract void OnTriggerExit();
}
