using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public override BaseUnit stateObject { get; set; }

    public AttackState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        this.stateObject = unit;
        unit.StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        BaseUnit targetUnit = stateObject.target.GetComponent<BaseUnit>();

        while(targetUnit.health > 0)
        {
            //play animation
            stateObject.atkAnimatior.SetTrigger("Attack");
            yield return new WaitForSeconds(stateObject.atkRate);

            //deal damage
            targetUnit.OnDamageTaken(stateObject.atk);
        }
        targetUnit.OnDead();
        stateObject.target = null;
    }

    public override void ExitState()
    {
        
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        if (stateObject.isDead) return UnitStateMachine.EUnitState.Dead;

        if (stateObject.target == null) return UnitStateMachine.EUnitState.FindTarget;

        if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x > stateObject.atkDistance)
        {
            return UnitStateMachine.EUnitState.ApproachTarget;
        }

        return UnitStateMachine.EUnitState.Attack;
    }
    public override void UpdateState()
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
