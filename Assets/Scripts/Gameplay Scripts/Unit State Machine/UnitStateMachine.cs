using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : StateMachine<UnitStateMachine.EUnitState>
{
    public enum EUnitState
    {
        FindTarget,
        ApproachTarget,
        Attack
    }
}
