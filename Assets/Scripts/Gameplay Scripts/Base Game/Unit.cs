using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    public new string name;
    public float atk;
    public float health;
    public List<SpriteRenderer> sprites;
}
