using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTargetState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public override BaseUnit stateObject { get; set; }

    public ApproachTargetState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        stateObject = unit;
    }

    public override void ExitState()
    {
        
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        if (stateObject.isDead) return UnitStateMachine.EUnitState.Dead;

        if (stateObject.target == null) return UnitStateMachine.EUnitState.FindTarget;

        //Debug.Log("Distance between object and target: " + Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x);
        if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x <= stateObject.agent.stoppingDistance + 1)
        {
            return UnitStateMachine.EUnitState.Attack;
        }

        return UnitStateMachine.EUnitState.ApproachTarget;
    }
    public override void UpdateState()
    {
        //stateObject.transform.position = Vector2.MoveTowards(stateObject.transform.position, stateObject.target.transform.position, stateObject.moveSpeed * Time.deltaTime);
        stateObject.agent.SetDestination(stateObject.target.transform.position);
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
