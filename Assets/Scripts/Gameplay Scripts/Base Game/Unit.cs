using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    public new string name;

    public float atk;

    public float atkDistance;

    public float atkRate;

    public float health;

    public float moveSpeed;

    public float enemyDetectDistance;

    public float price;

    public List<SpriteRenderer> sprites;

    public Sprite fullSprite;

    public Sprite fullSpriteEnermy;

    public Color playerColor;

    public Color enemyColor;

    public RuntimeAnimatorController unitAnimation;
}
