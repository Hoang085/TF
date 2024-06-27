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
            if (!stateObject.agent.pathPending)
            {
                if (stateObject.agent.remainingDistance <= stateObject.agent.stoppingDistance)
                {
                    if (!stateObject.agent.hasPath || stateObject.agent.velocity.sqrMagnitude == 0f)
                    {
                        if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x <=
                            stateObject.agent.stoppingDistance + 1)
                        {
                            return UnitStateMachine.EUnitState.Attack;
                        }
                        else
                        {
                            //Continue to progress even when stopping distance has been reached
                            stateObject.transform.Translate(Vector2.right * stateObject.agent.speed * Time.deltaTime);
                        }
                    }
                }
            }
        }
        

        return UnitStateMachine.EUnitState.FindTarget;
    }
    public override void UpdateState()
    {   
        if(stateObject.target != null)
        {
            stateObject.agent.SetDestination(stateObject.target.transform.position);
        }
        else
        {
            stateObject.transform.Translate(Vector2.right * stateObject.agent.speed * Time.deltaTime);
        }
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
