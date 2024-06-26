using System.Globalization;
using DG.Tweening;
using H2910.Defines;
using H2910.UI.Popups;
using TMPro;
using UnityEngine;

public class GoldGemPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemTxt;
    [SerializeField] TextMeshProUGUI goldTxt;

    private float _currentGold;
    private float _goldValue;
    private float _currentGem;
    private float _gemValue;
    
    private float _durationFx = 0.5f;
    
    private void OnEnable()
    {
        gemTxt.text = _currentGem.ToString("N0",CultureInfo.InvariantCulture);
        goldTxt.text = _currentGold.ToString("N0", CultureInfo.InvariantCulture);
    }
    
    public void UpdateGemTxt()
    {
        gemTxt.text = _gemValue.ToString("N0", CultureInfo.InvariantCulture);
    }

    public void UpdateGoldTxt()
    {
        goldTxt.text = _goldValue.ToString("N0", CultureInfo.InvariantCulture);
    }
    
}