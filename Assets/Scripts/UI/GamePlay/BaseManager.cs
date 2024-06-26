using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseManager : MonoBehaviour, IHealth
{
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private Image healthBar;

    [SerializeField] private float health;
    float maxHealth;

    public float Health { get => health; }

    public void OnDamageTaken(float damage)
    {
        health -= damage;
        healthTxt.text = health.ToString();
        healthBar.fillAmount = health / maxHealth;
    }

    public void OnDead()
    {
        Debug.Log("Game Over");
    }

    private void Awake()
    {
        healthTxt.GetComponentInChildren<TextMeshProUGUI>();
        healthBar.fillAmount = 1;
        healthTxt.text = health.ToString();
        maxHealth = health;
    }
}