using System;
using DG.Tweening;
using H2910.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : BasePopUp
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

    public void OnBtnHome()
    {
        if(_onClick)
            return;
        BlockMultyClick();
        OnCloseScreen();
        _tween?.Kill();
        Time.timeScale = 1;
        PopupManager.Instance.ShowLoadingScene("Home");
    }

    public void OnBtnRetry()
    {
        if(_onClick)
            return;
        BlockMultyClick();
        OnCloseScreen();
        _tween?.Kill();
        Time.timeScale = 1;

        string levelName = $"Level {PlayerPrefs.GetInt("UnlockedLevel", 1)}";
        PopupManager.Instance.ShowLoadingScene(levelName);
    }

    public void Close()
    {
        if (_onClick)
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
