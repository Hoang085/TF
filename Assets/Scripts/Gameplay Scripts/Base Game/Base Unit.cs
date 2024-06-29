using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using TF.Data;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class BaseUnit : MonoBehaviour, IHealth
{  
    [Header("Unit Identity")]
    [SerializeField] internal Unit unit;
    [SerializeField] internal UnitStateMachine unitState;
    [SerializeField] internal List<SpriteRenderer> sprites;
    [SerializeField] internal Image healthBar;
    [SerializeField] internal Animator atkAnimatior;
    private Collider2D mountCollider;

    internal bool isDead;

    [Header("Target Detection Var")]
    [SerializeField] internal LayerMask targetLayer;
    [SerializeField] internal bool isEnemy;
    [SerializeField] internal float radius;
    [SerializeField] internal GameObject target;
    [SerializeField] internal GameObject ally; //check if there's any ally in front of the unit

    [Header("Unit Stats")]
    internal float atk;
    internal float atkRate;
    internal float atkDistance;
    internal float enemyDetectDistance;
    internal float moveSpeed;
    internal float maxHealth;
    float IHealth.Health { get => health; }
    float health;

    internal float price;

    [Header("Layermasks")]
    private int playerMelee = 6;
    private int playerRange = 7;
    private int player = 10;
    private int enemyMelee = 8;
    private int enemyRange = 9;
    private int enemy = 11;

    private void OnEnable()
    {
        CharSelection.onUnitInitialize += OnUnitChange;
        CharSelection.onUnitInitialize += OnTeamChange;
    }
    private void OnDisable()
    {
        CharSelection.onUnitInitialize -= OnUnitChange;
        CharSelection.onUnitInitialize -= OnTeamChange;
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.transform.parent.gameObject.SetActive(false);
    }

    private void OnUnitChange()
    {
        if (unit != null)
        {
            //gameObject.name = unit.name;

            atk = isEnemy ? unit.atk : unit.atk * GameData.Instance.playerData.totalDamBoost;
            atkDistance = unit.atkDistance;
            atkRate = unit.atkRate;

            health = isEnemy ? unit.health : unit.health * GameData.Instance.playerData.totalHealthBoost;
            maxHealth = unit.health;
            moveSpeed = unit.moveSpeed;
            enemyDetectDistance = isEnemy ? -unit.enemyDetectDistance : unit.enemyDetectDistance;

            price = unit.price;

            for(int i = 0; i < sprites.Count; i++)
            {
                if (i >= unit.sprites.Count)
                {
                    sprites[i].sprite = null;
                    DestroyImmediate(mountCollider); //remove mount collider if it exist
                    continue; //skip if unit doesn't have the sprite needed for the last element
                }

                //Setting sprite renderer fields
                sprites[i].sprite = unit.sprites[i].sprite;
                sprites[i].sortingOrder = unit.sprites[i].sortingOrder;

                if (sprites[i].gameObject.name == "Body")
                {
                    sprites[i].color = isEnemy ? unit.enemyColor : unit.playerColor;
                }

                else { sprites[i].color = unit.sprites[i].color; }

                if (sprites[i].gameObject.name == "Mount" && sprites[i].gameObject.GetComponent<BoxCollider2D>() == null)
                {
                    mountCollider = sprites[i].gameObject.AddComponent<BoxCollider2D>(); //add collider to mount
                    mountCollider.GetComponent<BoxCollider2D>().size = new Vector2(1, 1.25f);
                }

                //Setting sprites transform value 
                sprites[i].transform.localPosition = unit.sprites[i].transform.localPosition;
                sprites[i].transform.localRotation = unit.sprites[i].transform.rotation;
            }
            //animator component with an animation clip attached to it will reset any change made to sprite renderer during runtime so we will need to 
            //register it after all sprite renderer has been loaded
            atkAnimatior.runtimeAnimatorController = unit.unitAnimation;
        }
    }
    private void OnTeamChange()
    {
        if (isEnemy)
        {
            if(unit?.atkDistance <= 1)
            {
                gameObject.layer = enemyMelee;
            }
            else
            {
                gameObject.layer = enemyRange;
            }
            Physics2D.IgnoreLayerCollision(enemyMelee, enemyRange);

            var targetLayer1 = 1 << playerMelee;
            var targetLayer2 = 1 << playerRange;
            var targetLayer3 = 1 << player;

            targetLayer = targetLayer1 | targetLayer2 | targetLayer3;

            transform.eulerAngles = Vector3.up * 180;
        }
        else
        {
            if (unit?.atkDistance <= 1)
            {
                gameObject.layer = playerMelee;
            }
            else
            {
                gameObject.layer = playerRange;
            }
            Physics2D.IgnoreLayerCollision(playerMelee, playerRange);

            var targetLayer1 = 1 << enemyMelee;
            var targetLayer2 = 1 << enemyRange;
            var targetLayer3 = 1 << enemy;

            targetLayer = targetLayer1 | targetLayer2 | targetLayer3;

            transform.eulerAngles = Vector3.zero;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void OnDead()
    {
        isDead = true;
        healthBar.transform.parent.gameObject.SetActive(false);
    }

    public void OnDamageTaken(float damage)
    {
        healthBar.transform.parent.gameObject.SetActive(true);
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
    }
    

#if UNITY_EDITOR
    private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

    private void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        OnUnitChange();
        OnTeamChange();
    }
#endif
}
