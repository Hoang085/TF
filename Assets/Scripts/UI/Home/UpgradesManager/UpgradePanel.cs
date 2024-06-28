using System;
using TF.Data;
using TMPro;
using UnityEngine;

public class UpgradePanel : PanelBase
{
    [SerializeField] private TextMeshProUGUI foodProTxt;
    [SerializeField] private TextMeshProUGUI baseHeathTxt;
    [SerializeField] private TextMeshProUGUI foodUpgradePriceTxt;
    [SerializeField] private TextMeshProUGUI baseHeathPriceTxt;

    private float _priceDecrea = 4;

    private void Start()
    {
        UpdateTxt();
    }

    private void UpdateTxt()
    {
        CheckPrice(foodUpgradePriceTxt);
        CheckPrice(baseHeathPriceTxt);
        foodProTxt.text = $"{Math.Round(GameData.Instance.playerData.foodProductionSpeed/10, 2).ToString()}/s";
        baseHeathTxt.text = GameData.Instance.playerData.baseHealth.ToString();

        foodUpgradePriceTxt.text = GameData.Instance.priceData.foodUpgradePrice.ToString();
        baseHeathPriceTxt.text = GameData.Instance.priceData.baseUpgradePrice.ToString();

    }

    public void UpgradeFoodPro()
    {
        if (GameData.Instance.playerData.coin >= GameData.Instance.priceData.foodUpgradePrice)
        {
            GameData.Instance.playerData.foodProductionSpeed -= 0.2f;
            GameData.Instance.priceData.foodUpgradePrice += (int)(GameData.Instance.priceData.foodUpgradePrice/2);
            GameData.Instance.playerData.coin -= GameData.Instance.priceData.foodUpgradePrice;
            UpdateTxt();
        }
    }

    public void UpgradeBaseHeath()
    {
        if (GameData.Instance.playerData.coin >= GameData.Instance.priceData.foodUpgradePrice)
        {
            GameData.Instance.playerData.baseHealth += 2;
            GameData.Instance.priceData.baseUpgradePrice += (int)(GameData.Instance.priceData.baseUpgradePrice / 2);
            GameData.Instance.playerData.coin -= GameData.Instance.priceData.baseUpgradePrice;
            UpdateTxt();
        }

    }

    private void CheckPrice(TextMeshProUGUI txt)
    {
        if (GameData.Instance.playerData.coin < GameData.Instance.priceData.foodUpgradePrice)
        {
            txt.color = Color.red;
        }
        else
        {
            txt.color = Color.white;
        }
    }
}