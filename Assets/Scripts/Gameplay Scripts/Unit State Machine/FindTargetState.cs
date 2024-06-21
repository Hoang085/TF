using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        if (stateObject.target == null) return UnitStateMachine.EUnitState.FindTarget;

        return UnitStateMachine.EUnitState.ApproachTarget;
    }
    public override void UpdateState()
    {
        stateObject.transform.Translate(Vector2.right * stateObject.moveSpeed * Time.deltaTime);
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
        }
        else
        {
            curHitDistance = distance;
            stateObject.target = null;
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
