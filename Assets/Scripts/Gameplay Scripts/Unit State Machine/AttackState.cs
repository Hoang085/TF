using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public override BaseUnit stateObject { get; set; }
    private bool targetChanged;

    public AttackState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit unit)
    {
        this.stateObject = unit;

        if(stateObject.atkDistance <= 2)
        {
            stateObject.StartCoroutine(MeleeAttack());
        }
        else
        {
            stateObject.StartCoroutine(RangeAttack());
        }
    }
    private IEnumerator MeleeAttack()
    {
        var targetHp = stateObject.target.GetComponent<IHealth>();

        while(targetHp.Health > 0)
        {
            //if current target is not the same as the object old target
            if (!targetHp.Equals(stateObject?.target.GetComponent<IHealth>()))
            {
                Debug.Log("Target changed melee");//play animation
                targetChanged = true;
                break;
            }

            stateObject.atkAnimatior.SetTrigger("Attack");

            yield return new WaitForSeconds(stateObject.atkRate);

            //deal damage
            targetHp.OnDamageTaken(stateObject.atk);
            if (targetHp.Health <= 0) break;

            //Delay before the next attack
            yield return new WaitForSeconds(1);
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
            //if current target is not the same as the object old target
            if (!targetHp.Equals(stateObject?.target.GetComponent<IHealth>()))
            {
                Debug.Log("Target changed range");
                targetChanged = true;
                break;
            }

            //play animation
            stateObject.atkAnimatior.runtimeAnimatorController = stateObject.unit.unitAnimation;
            stateObject.atkAnimatior.SetTrigger("Attack");
            yield return new WaitForSeconds(stateObject.atkRate / 2);

            //break out of the attack loop if the target is no longer there, this is mainly to prevent null reference bug when projectile are flying
            if (stateObject.target == null || !stateObject.target.activeSelf) break;

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
            if (projectileDuration < (stateObject.atkRate / 2)) yield return new WaitForSeconds((stateObject.atkRate / 2) - projectileDuration);

            rangeWeapon.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutSine);

            //Delay before the next attack
            yield return new WaitForSeconds(1);
        }
    }

    public override void ExitState()
    {
        if (stateObject.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite != null)
        {
            //Debug.Log("Exit Attack State, stop attacking");
        }
        
        stateObject.StopAllCoroutines(); //stop all attack currently running in this state first
        targetChanged = false;
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        if (stateObject.isDead) return UnitStateMachine.EUnitState.Dead;

        if (stateObject.target == null)
        {
            return UnitStateMachine.EUnitState.FindTarget;
        }
        else if(stateObject.target.GetComponent<IHealth>().Health <= 0 ||
            Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) /
            stateObject.transform.lossyScale.x > stateObject.atkDistance + 1f ||
            targetChanged)
        {
            return UnitStateMachine.EUnitState.FindTarget;
        }

        return UnitStateMachine.EUnitState.Attack;
    }
    public override void UpdateState()
    {
        //give state object a little nudge to chase after  the target if they are too far away
        if (Vector2.Distance(stateObject.transform.position, stateObject.target.transform.position) /
            stateObject.transform.lossyScale.x > stateObject.atkDistance)
        {
            if (stateObject.unit.name == "Caveman")
            {
                Debug.Log("Chasing current target");
            }

            stateObject.transform.position = Vector2.MoveTowards(stateObject.transform.position, stateObject.target.transform.position, stateObject.moveSpeed * Time.deltaTime);
        } 
    }
    public override void OnCollisionEnter2D(Collision2D collision)
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
