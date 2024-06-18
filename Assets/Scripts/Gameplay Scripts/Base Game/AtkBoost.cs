using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boosts/AttackBoost")]
public class AtkBoost : CardBoost
{
    public override void Apply(BaseUnit target)
    {
        if (!target.isEnemy)
        {
            target.atk *= boostMultiplier;
        }
    }
}
