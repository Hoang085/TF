using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTargetState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    BaseUnit unit;
    public ApproachTargetState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        this.unit = unit;
    }

    public override void ExitState()
    {
        
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        if (unit.target == null)
        {
            return UnitStateMachine.EUnitState.FindTarget;
        }

        if(Vector2.Distance(unit.transform.position, unit.target.transform.position) / unit.transform.lossyScale.x <= unit.atkDistance)
        {
            return UnitStateMachine.EUnitState.Attack;
        }
        return UnitStateMachine.EUnitState.ApproachTarget;
    }
    public override void UpdateState()
    {
        unit.transform.position = Vector2.MoveTowards(unit.transform.position, unit.target.transform.position, unit.moveSpeed * Time.deltaTime);
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

}
