using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boosts/HealthBoost")]
public class HealthBoost : CardBoost
{
    public override void Apply(BaseUnit target)
    {
        if (!target.isEnemy)
        {
            target.health *= boostMultiplier;
        }
    }
}
