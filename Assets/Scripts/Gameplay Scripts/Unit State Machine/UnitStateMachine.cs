using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : StateMachine<UnitStateMachine.EUnitState, BaseUnit>
{
    public enum EUnitState
    {
        FindTarget,
        ApproachTarget,
        Attack,
        Dead
    }

    FindTargetState findState = new FindTargetState(EUnitState.FindTarget);
    ApproachTargetState approachState = new ApproachTargetState(EUnitState.ApproachTarget);
    AttackState attackState = new AttackState(EUnitState.Attack);
    DeadState deadState = new DeadState(EUnitState.Dead);

    private void Awake()
    {
        //System.Enum.GetValues(typeof(EUnitState)).Length
        states.Add(EUnitState.FindTarget, findState);
        states.Add(EUnitState.ApproachTarget, approachState);   
        states.Add(EUnitState.Attack, attackState);
        states.Add(EUnitState.Dead, deadState);
    }
    private void Start()
    {
        TransitionToState(EUnitState.FindTarget);
    }
}
