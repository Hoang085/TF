using System;
using TF.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseManager : MonoBehaviour, IHealth
{
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private Image healthBar;
    [SerializeField] private float health;
    [SerializeField] private bool isEnemy;

    private float _maxHealth;

    public float Health { get => health; }

    public void OnDamageTaken(float damage)
    {
        health -= damage;
        healthTxt.text = health.ToString();
        healthBar.fillAmount = health / _maxHealth;
        gameObject.GetComponent<CollectionItem>().DropCoin(gameObject.transform, true, false);
    }

    public void OnDead()
    {
        Debug.Log("Game Over");
    }

    private void Start()
    {
        healthTxt.GetComponentInChildren<TextMeshProUGUI>();
        healthBar.fillAmount = 1;
        if (!isEnemy)
            health = GameData.Instance.playerData.baseHealth;

        healthTxt.text = health.ToString();
        _maxHealth = health;
    }
}