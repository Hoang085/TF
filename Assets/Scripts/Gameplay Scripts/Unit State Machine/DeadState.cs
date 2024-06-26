using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public override BaseUnit stateObject { get; set; }

    public DeadState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        stateObject = unit;
        ObjectPoolManager.ReturnObjectToPool(stateObject.gameObject);
        //stateObject.gameObject.SetActive(false);
    }

    public override void ExitState()
    {
        stateObject.isDead = false;
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        if(stateObject.gameObject.activeSelf) return UnitStateMachine.EUnitState.FindTarget;

        return UnitStateMachine.EUnitState.Dead;
    }

    public override void OnTriggerEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        
    }
}
