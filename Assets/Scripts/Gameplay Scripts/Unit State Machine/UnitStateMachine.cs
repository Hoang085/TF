using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : StateMachine<UnitStateMachine.EUnitState, BaseUnit>
{
    public enum EUnitState
    {
        FindTarget,
        ApproachTarget,
        Attack
    }

    FindTargetState findState = new FindTargetState(EUnitState.FindTarget);
    ApproachTargetState approachState = new ApproachTargetState(EUnitState.ApproachTarget);
    AttackState attackState = new AttackState(EUnitState.Attack);

    private void Awake()
    {
        //System.Enum.GetValues(typeof(EUnitState)).Length
        states.Add(EUnitState.FindTarget, findState);
        states.Add(EUnitState.ApproachTarget, approachState);   
        states.Add(EUnitState.Attack, attackState);
    }
    private void Start()
    {
        TransitionToState(EUnitState.FindTarget);
    }
}
