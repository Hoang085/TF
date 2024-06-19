using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    public new string name;

    public float atk;

    public float atkSpeed;

    public float health;

    public float moveSpeed;

    public float price;

    public List<SpriteRenderer> sprites;

    public Sprite fullSprite;

    public Color playerColor;

    public Color enemyColor;
}
