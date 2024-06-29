using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardBoost : ScriptableObject
{
    [Header("Card Type")]
    public new string name;
    public BaseCard currentCard;
    public float boostMultiplier;
    public EnumData.Rarity rarity;


    [Header("Card Appearance")]
    public Sprite cardImage;
    public string cardDescription;

    public abstract void Apply();
    public abstract void RemoveBoost();
}
