using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public override BaseUnit stateObject { get; set; }

    public AttackState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        this.stateObject = unit;

        if(unit.agent.stoppingDistance <= 1)
        {
            unit.StartCoroutine(MeleeAttack());
        }
        else
        {
            unit.StartCoroutine(RangeAttack());
        }
    }
    private IEnumerator MeleeAttack()
    {
        var targetHp = stateObject.target.GetComponent<IHealth>();

        while(targetHp.Health > 0)
        {
            //play animation
            stateObject.atkAnimatior.SetTrigger("Attack");
            yield return new WaitForSeconds(stateObject.atkRate);

            //deal damage
            targetHp.OnDamageTaken(stateObject.atk);
        }
        targetHp.OnDead();
        stateObject.target = null;
    }

    private IEnumerator RangeAttack()
    {
        var targetHp = stateObject.target.GetComponent<IHealth>();
        var rangeWeapon = stateObject.sprites[2].gameObject; //weapon will always be the 3rd child of sprites

        while (targetHp.Health > 0)
        {
            //play animation
            stateObject.atkAnimatior.SetTrigger("Attack");
            yield return new WaitForSeconds(stateObject.atkRate / 2);

            //animation clip needs to be removed in order to adjust the value of the animated object properties, we can assign it back later
            stateObject.atkAnimatior.runtimeAnimatorController = null;

            float projectileDuration = Vector2.Distance(rangeWeapon.transform.position, stateObject.target.transform.position) / 2.5f;

            rangeWeapon.transform.DOMove(stateObject.target.transform.position, projectileDuration).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(projectileDuration);

            //deal damage
            targetHp.OnDamageTaken(stateObject.atk);

            if(targetHp.Health <= 0)
            {
                //target unit should die instantly instead of waiting for all those yield return below
                targetHp.OnDead();
                //ObjectPoolManager.ReturnObjectToPool(stateObject.target.gameObject);
                stateObject.target = null;
            }

            rangeWeapon.transform.localScale = Vector3.zero;
            rangeWeapon.transform.localPosition = new Vector3(-0.2f, -0.1f, 0);

            //wait for the complete cycle before attacking again
            if (projectileDuration < 0.5f) yield return new WaitForSeconds(0.5f - projectileDuration);

            rangeWeapon.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutSine);

            //wait for weapon to load back
            yield return new WaitForSeconds(0.2f);

            stateObject.atkAnimatior.runtimeAnimatorController = stateObject.unit.unitAnimation;
        }
    }

    public override void ExitState()
    {
        
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        if (stateObject.isDead) return UnitStateMachine.EUnitState.Dead;

        if (stateObject.target == null)
        {
            return UnitStateMachine.EUnitState.FindTarget;
        }
        else if(stateObject.target.GetComponent<IHealth>().Health <= 0)
        {
            return UnitStateMachine.EUnitState.FindTarget;
        }

        //if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) / stateObject.transform.lossyScale.x > stateObject.agent.stoppingDistance + 1)
        //{
        //    return UnitStateMachine.EUnitState.ApproachTarget;
        //}

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
