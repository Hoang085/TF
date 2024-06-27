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
        foodProTxt.text = $"{Math.Round(GameData.Instance.playerData.foodProductionSpeed/10, 2).ToString()}/s";
        baseHeathTxt.text = GameData.Instance.playerData.baseHealth.ToString();

        foodUpgradePriceTxt.text = GameData.Instance.priceData.foodUpgradePrice.ToString();
        baseHeathPriceTxt.text = GameData.Instance.priceData.baseUpgradePrice.ToString();
    }

    public void UpgradeFoodPro()
    {
        GameData.Instance.playerData.foodProductionSpeed -= 0.2f;
        GameData.Instance.priceData.foodUpgradePrice += (int)(GameData.Instance.priceData.foodUpgradePrice/2);
        UpdateTxt();
    }

    public void UpgradeBaseHeath()
    {
        GameData.Instance.playerData.baseHealth += 2;
        GameData.Instance.priceData.baseUpgradePrice += (int)(GameData.Instance.priceData.baseUpgradePrice / 2);
        UpdateTxt();
    }
}