using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using H2910.Defines;
using H2910.UI.Popups;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : BasePopUp
{
    [SerializeField] private GameEvent bonusTimeEvent;
    
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

    public void OnBtnRetry()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        OnCloseScreen();
        Time.timeScale = 1;
        _tween?.Kill();

        string levelName = $"Level {PlayerPrefs.GetInt("UnlockedLevel", 1)}";
        PopupManager.Instance.ShowLoadingScene(levelName);
    }

    public void OnBtnMoreTime()
    {
        if (_onClick)
        {
            return;
        }
        BlockMultyClick();
        bonusTimeEvent.Raise();
        Time.timeScale = 1;
        Close();
        
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
