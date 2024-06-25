using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharSelection : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private Image image;
    [SerializeField] private GameObject isBuy;
    [SerializeField] private GameObject baseUnit;

    private float _price;
    private TextMeshProUGUI _priceText;
    private Unit _unit;
    private Vector2 unitSpawnPos = new Vector2(-2.03f, 0);
    
    private bool _onClick;

    public static event Action onUnitInitialize;
    
    private void Awake()
    {
        _unit = Resources.Load<Unit>($"Scriptable Objects/Unit Data/Stone Age/{name}");

        _priceText = GetComponentInChildren<TextMeshProUGUI>();
        _price = _unit.price;
        
        _priceText.text = _price.ToString();
        image.sprite = Resources.Load<Sprite>($"Sprite/{_unit.fullSprite.name}");

        UIGamePlayManager.onFoodChange += PriceCheck;
    }
    private void Start()
    {
        PriceCheck();
    }

    private void PriceCheck()
    {
        if (_price == UIGamePlayManager.Instance.foodAmount)
        {
            isBuy.SetActive(false);
        }
        else if (_price > UIGamePlayManager.Instance.foodAmount)
        {
            isBuy.SetActive(true);
        }
    }
    
    private void BlockMultyClick()
    {
        _onClick = true;
        DOVirtual.DelayedCall(0.1f, () => { _onClick = false; });
    }

    public void OnBuyCharClick()
    {
        if(_onClick)
            return;
        BlockMultyClick();
        
        if(!isBuy.activeSelf)
        {
            UIGamePlayManager.Instance.foodAmount -= _price;
            UIGamePlayManager.onFoodChange?.Invoke(); 

            //Spawn Char
            //BaseUnit unit = Instantiate(baseUnit, unitSpawnPos, Quaternion.identity).GetComponent<BaseUnit>();
            //unit.unit = _unit;
            //onUnitInitialize?.Invoke();
        }

        BaseUnit unit = Instantiate(baseUnit, unitSpawnPos, Quaternion.identity).GetComponent<BaseUnit>();
        unit.unit = _unit;
        onUnitInitialize?.Invoke();
    }
}