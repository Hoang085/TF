using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseUnit : MonoBehaviour
{
    [Header("Unit Identity")]
    [SerializeField] internal Unit unit;
    [SerializeField] internal UnitStateMachine unitState;
    [SerializeField] List<SpriteRenderer> sprites;
    [SerializeField] internal Image healthBar;
    [SerializeField] internal Animator atkAnimatior;

    internal bool isDead;

    [Header("Target Detection Var")]
    [SerializeField] internal LayerMask targetLayer;
    [SerializeField] internal bool isEnemy;
    [SerializeField] internal float radius;
    internal GameObject target;

    [Header("Player Stats")]
    internal float atk;
    internal float atkDistance;
    internal float atkRate;
    internal float health;
    internal float maxHealth;
    internal float moveSpeed;
    internal float enemyDetectDistance;
    internal float price;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.transform.parent.gameObject.SetActive(false);

        if (!isEnemy)
        {
            foreach(BaseCard card in DataManager.instance.cards)
            {
                card.cardBoost.Apply(this);
            }
        }
    }

    private void OnUnitChange()
    {
        if (unit != null)
        {
            gameObject.name = unit.name;

            atk = unit.atk;
            atkDistance = unit.atkDistance;
            atkRate = unit.atkRate;
            atkAnimatior.runtimeAnimatorController = unit.unitAnimation;

            health = unit.health;
            maxHealth = unit.health;
            moveSpeed = unit.moveSpeed;
            enemyDetectDistance = isEnemy ? -unit.enemyDetectDistance : unit.enemyDetectDistance;

            price = unit.price;

            for(int i = 0; i < sprites.Count; i++)
            {
                if (i >= unit.sprites.Count)
                {
                    sprites[i].sprite = null;
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

                //Setting sprites transform value 
                sprites[i].transform.localPosition = unit.sprites[i].transform.localPosition;
                sprites[i].transform.localRotation = unit.sprites[i].transform.localRotation;
            }
        }
    }
    private void OnTeamChange()
    {
        if (isEnemy)
        {
            gameObject.layer = 7;
            targetLayer = 1 << 6;

            transform.eulerAngles = Vector3.up * 180;
        }
        else
        {
            gameObject.layer = 6;
            targetLayer = 1 << 7;

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

    public void OnDamageTaken(float data)
    {
        healthBar.transform.parent.gameObject.SetActive(true);
        health -= data;
        healthBar.fillAmount = health / maxHealth;
    }
    

#if UNITY_EDITOR
    private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

    private void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        Debug.Log("On Validate");
        OnUnitChange();
        OnTeamChange();
    }
#endif
}
