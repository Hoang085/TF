using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private Image healthBar;

    [SerializeField] private int health;

    private void Awake()
    {
        healthTxt.GetComponentInChildren<TextMeshProUGUI>();
        healthBar.fillAmount = 1;
        healthTxt.text = health.ToString();
    }
    
}