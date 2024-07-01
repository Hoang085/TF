using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class FindTargetState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public override BaseUnit stateObject { get; set; }

    private bool isCollideWithTarget;

    public FindTargetState(UnitStateMachine.EUnitState key) : base(key)
    {

    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        stateObject = unit;
    }

    public override void ExitState()
    {
        isCollideWithTarget = false;
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        if (stateObject.isDead) return UnitStateMachine.EUnitState.Dead;
   
        if(stateObject.target != null)
        {
            //Debug.Log("Distance between object and target: " + Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x);

            if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x <=
                            stateObject.atkDistance || isCollideWithTarget)
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
            stateObject.transform.position = Vector2.MoveTowards(stateObject.transform.position,
                stateObject.target.transform.position, stateObject.moveSpeed * Time.deltaTime);
        }
        else
        {
            stateObject.transform.Translate(Vector2.right * stateObject.moveSpeed * Time.deltaTime);
        }
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (stateObject.targetLayer == (stateObject.targetLayer | 1 << collision.gameObject.layer))
        {
            isCollideWithTarget = true;
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
