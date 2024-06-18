using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    public new string name;

    public float atk;

    public float atkSpeed;

    public float health;

    public float moveSpeed;

    public List<SpriteRenderer> sprites;

    public Color playerColor;

    public Color enemyColor;
}
