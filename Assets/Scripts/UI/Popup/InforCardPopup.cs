using DG.Tweening;
using H2910.UI.Popups;
using UnityEngine;

public class InforCardPopup : BasePopUp
{
    private Tween _tween;
    private bool _onClick;

    private void Start()
    {
        Time.timeScale = 0;
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

    public void OnBtnUpgradeClick()
    {
        
    }
}