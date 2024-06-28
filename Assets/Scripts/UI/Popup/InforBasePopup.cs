using DG.Tweening;
using H2910.UI.Popups;
using TF.Data;
using TMPro;
using UnityEngine;

public class InforBasePopup : BasePopUp
{
    [SerializeField] private TextMeshProUGUI damageBoostTxt;
    [SerializeField] private TextMeshProUGUI heathBoostTxt;
    
    private Tween _tween;
    private bool _onClick;

    private void Start()
    {
        Time.timeScale = 0;
        UpdateTxt();
    }

    private void BlockMultyClick()
    {
        _onClick = true;
        DOVirtual.DelayedCall(0.1f, () => { _onClick = false; });
    }    
    
    public void SetBlockClick(bool isBlock)
    {
        _onClick = isBlock;
    }
    
    private void UpdateTxt()
    {
        //damageBoostTxt.text = $"All Unit Damage x{GameData.Instance.playerData.dameBoost.ToString()}";
        //heathBoostTxt.text = $"All Unit Health x{GameData.Instance.playerData.healthBoost.ToString()}";
    }
    
    public void Close()
    {
        if (OnClick)
            return;
        BlockMultyClick();
        OnCloseScreen();
        _tween?.Kill();
    }

    public override void OnCloseScreen()
    {
        Time.timeScale = 1;
        base.OnCloseScreen();
    }
}