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
        Debug.Log("Hello from Find target state");
        this.stateObject = unit;
        direction = Vector2.right;
        distance = unit.enemyDetectDistance;
        radius = unit.radius;
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
            //if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x <=
            //stateObject.agent.stoppingDistance + 1)
            //{
            //    return UnitStateMachine.EUnitState.Attack;
            //}
            if (!stateObject.agent.pathPending)
            {
                if (stateObject.agent.remainingDistance <= stateObject.agent.stoppingDistance)
                {
                    if (!stateObject.agent.hasPath || stateObject.agent.velocity.sqrMagnitude == 0f)
                    {
                        return UnitStateMachine.EUnitState.Attack;
                    }
                }
            }
        }
        

        return UnitStateMachine.EUnitState.FindTarget;
    }
    public override void UpdateState()
    {   
        CheckForTarget();
    }
    private void CheckForTarget()
    {
        rayOrigin = stateObject.transform.position;
        distance = stateObject.enemyDetectDistance;
        radius = stateObject.radius;

        RaycastHit2D hit = Physics2D.CircleCast(rayOrigin, radius, direction, distance, stateObject.targetLayer);
        Debug.DrawRay(rayOrigin, direction * distance * 2 * radius, Color.green);

        if (hit)
        {
            stateObject.target = hit.transform.gameObject;
            curHitDistance = hit.distance;

            stateObject.agent.SetDestination(stateObject.target.transform.position);
        }
        else
        {
            curHitDistance = distance;
            stateObject.target = null;

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
