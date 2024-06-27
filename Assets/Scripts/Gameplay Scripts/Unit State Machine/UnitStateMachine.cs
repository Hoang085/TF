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

    [Header("Unit States")]
    FindTargetState findState = new FindTargetState(EUnitState.FindTarget);
    AttackState attackState = new AttackState(EUnitState.Attack);
    DeadState deadState = new DeadState(EUnitState.Dead);

    [Header("Unit Detection Var")]
    private float distance;
    private float radius;
    private Vector3 rayOrigin;
    private Vector3 direction = Vector2.right;

    private void Awake()
    {
        //System.Enum.GetValues(typeof(EUnitState)).Length
        states.Add(EUnitState.FindTarget, findState);
        states.Add(EUnitState.Attack, attackState);
        states.Add(EUnitState.Dead, deadState);
    }
    private void Start()
    {
        TransitionToState(EUnitState.FindTarget);
    }
    protected override void Update()
    {
        base.Update();
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
        }
        else
        {
            stateObject.target = null;
        }
    }
}
