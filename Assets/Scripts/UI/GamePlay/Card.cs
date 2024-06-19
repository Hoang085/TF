using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private Image image;
    [SerializeField] private GameObject isBuy;
    
    private float _price;
    private TextMeshProUGUI _priceText;
    private Unit _unit;
    
    private void Awake()
    {
        _unit = Resources.Load<Unit>($"Scriptable Objects/Unit Data/Stone Age/{name}");

        _priceText = GetComponentInChildren<TextMeshProUGUI>();
        _price = _unit.price;
        
        _priceText.text = _price.ToString();
        image.sprite = Resources.Load<Sprite>($"Sprite/{_unit.fullSprite.name}");
    }

    private void Update()
    {
        if (_price == UIGamePlayManager.Instance.foodAmount)
        {
            isBuy.SetActive(false);
        }else if (_price > UIGamePlayManager.Instance.foodAmount)
        {
            isBuy.SetActive(true);
        }
    }
}