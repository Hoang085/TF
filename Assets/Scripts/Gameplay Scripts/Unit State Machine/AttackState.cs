using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    BaseUnit unit;
    public AttackState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        this.unit = unit;
        Debug.Log("Hello from attack state");
        unit.StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        BaseUnit targetUnit = unit.target.GetComponent<BaseUnit>();

        while(targetUnit.health > 0)
        {
            //play animation
            yield return new WaitForSeconds(unit.atkRate);

            //deal damage
            targetUnit.health -= unit.atk;
            Debug.Log(targetUnit.name + " remaining health: " + targetUnit.health);
        }
        targetUnit.gameObject.SetActive(false);
        unit.target = null;
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

        if (Vector2.Distance(unit.transform.position, unit.target.transform.position) / unit.transform.lossyScale.x > unit.atkDistance)
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
