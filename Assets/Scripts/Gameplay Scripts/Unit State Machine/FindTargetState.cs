using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FindTargetState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    BaseUnit unit;

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
        this.unit = unit;
        direction = Vector2.right;
        distance = unit.enemyDetectDistance;
        radius = unit.radius;
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

        return UnitStateMachine.EUnitState.ApproachTarget;
    }
    public override void UpdateState()
    {
        unit.transform.Translate(Vector2.right * unit.moveSpeed * Time.deltaTime);
        CheckForTarget();
    }
    private void CheckForTarget()
    {
        rayOrigin = unit.transform.position;
        distance = unit.enemyDetectDistance;
        radius = unit.radius;

        RaycastHit2D hit = Physics2D.CircleCast(rayOrigin, radius, direction, distance, unit.targetLayer);
        Debug.DrawRay(rayOrigin, direction * distance * 2 * radius, Color.green);

        if (hit)
        {
            Debug.Log("Target Found");
            unit.target = hit.transform.gameObject;
            curHitDistance = hit.distance;
        }
        else
        {
            curHitDistance = distance;
            unit.target = null;
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
