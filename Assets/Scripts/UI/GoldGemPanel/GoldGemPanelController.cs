using System.Globalization;
using DG.Tweening;
using H2910.Common.Singleton;
using H2910.Defines;
using H2910.UI.Popups;
using TF.Data;
using TMPro;
using UnityEngine;

public class GoldGemPanelController : ManualSingletonMono<GoldGemPanelController>
{
    [SerializeField] TextMeshProUGUI gemTxt;
    [SerializeField] TextMeshProUGUI CoinTxt;

    private float _currentCoin;
    private float _CoinValue;
    private float _currentGem;
    private float _gemValue;
    
    private float _durationFx = 0.5f;

    private void Start()
    {
        if (GameData.Instance == null)
        {
            return;
        }
        
        _currentCoin = GameData.Instance.playerData.coin;
        _currentGem = GameData.Instance.playerData.gem;
        
        gemTxt.text = _currentGem.ToString("N0",CultureInfo.InvariantCulture);
        CoinTxt.text = _currentCoin.ToString("N0", CultureInfo.InvariantCulture);
    }
    
    public void UpdateGemTxt()
    {
        gemTxt.text = _gemValue.ToString("N0", CultureInfo.InvariantCulture);
    }

    public void UpdateCoinTxt()
    {
        CoinTxt.text = _CoinValue.ToString("N0", CultureInfo.InvariantCulture);
    }
    
}