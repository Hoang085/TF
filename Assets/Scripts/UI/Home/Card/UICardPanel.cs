

using System;
using TF.Data;
using TMPro;
using UnityEngine;

public class UICardPanel : PanelBase
{
    [SerializeField] private TabGroup homeTabGroup;
    
    [SerializeField] private TextMeshProUGUI buyCard1Btn;
    [SerializeField] private TextMeshProUGUI buyCard2Btn;
    [SerializeField] private TextMeshProUGUI price1Txt;
    [SerializeField] private TextMeshProUGUI price2Txt;

    private int _price1;
    private int _price2;

    private void Start()
    {
        _price1 = 10;
        _price2 = 100;
        UpdateTxt();
        UpdateTxt();
    }

    private void UpdateTxt()
    {
        if (GameData.Instance.playerData.gem < _price1)
        {
            buyCard1Btn.color = Color.red;
            price1Txt.color = Color.red;
        }
        else
        {
            buyCard1Btn.color = Color.white;
            price1Txt.color = Color.white;
        }
        
        if (GameData.Instance.playerData.gem < _price2)
        {
            buyCard2Btn.color = Color.red;
            price2Txt.color = Color.red;
        }
        else
        {
            buyCard2Btn.color = Color.white;
            price2Txt.color = Color.white;
        }
    }

    public void OnBuyClick(int price)
    {
        if (price <= GameData.Instance.playerData.gem)
        {
            //add Card
            GameData.Instance.playerData.gem -= price;
            GoldGemPanelController.Instance.UpdateTxt();
            UpdateTxt();
        }
        else
        {
            homeTabGroup.OnTabSelected(3, true);
        }
    }
}