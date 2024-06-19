using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState<UnitStateMachine.EUnitState, BaseUnit>
{
    public AttackState(UnitStateMachine.EUnitState key) : base(key)
    {
    }

    public override void EnterState(StateMachine<UnitStateMachine.EUnitState, BaseUnit> stateMachine, BaseUnit stateObject)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override UnitStateMachine.EUnitState GetNextState()
    {
        throw new System.NotImplementedException();
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

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
