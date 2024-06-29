using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class FindTargetState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public override BaseUnit stateObject { get; set; }

    private float distance;
    private float radius = 0.05f;
    private float curHitDistance;
    private Vector3 rayOrigin;
    private Vector3 direction;


    public FindTargetState(UnitStateMachine.EUnitState key) : base(key)
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

        //Debug.Log("Distance between object and target: " + Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x);
        if(stateObject.target != null)
        {
            if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x <=
                            stateObject.atkDistance + 0.5f)
            {
                return UnitStateMachine.EUnitState.Attack;
            }
        }
        

        return UnitStateMachine.EUnitState.FindTarget;
    }
    public override void UpdateState()
    {   
        if(stateObject.target != null)
        {
            stateObject.transform.position = Vector2.MoveTowards(stateObject.transform.position, stateObject.target.transform.position, stateObject.moveSpeed * Time.deltaTime);
        }
        else
        {
            stateObject.transform.Translate(Vector2.right * stateObject.moveSpeed * Time.deltaTime);
        }
    }
    private void CheckForAlly()
    {

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
