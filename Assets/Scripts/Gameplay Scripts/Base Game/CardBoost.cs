using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBoost : ScriptableObject
{
    public float boostMultiplier;
    public int level;
    public SpriteRenderer sprite;
    public EnumData.Rarity rarity;
    public Color rarityColor;
    public abstract void Apply(BaseUnit target);
}
