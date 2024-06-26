﻿using DG.Tweening;
using H2910.Common.Singleton;
using H2910.Defines;
using H2910.UI.Popups;
using TMPro;
using UnityEngine;

public class UIHomeManager : ManualSingletonMono<UIHomeManager>
{
    private Tween _tween;
    private bool _onClick;
    private CanvasGroup _canvas;

    public override void Awake()
    {
        _canvas = GetComponentInChildren<CanvasGroup>();
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
    
    public void OnBtnSettingClick()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        PopupManager.Instance.OnShowScreen(PopupName.Setting, ParentPopup.Hight);
    }
    
    public void OnBtnShop()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        PopupManager.Instance.OnShowScreen(PopupName.ShopAll);
    }

    public void OnBtnLevelSelection()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        ShowUIHome(false);
        
        string levelName = $"Level {PlayerPrefs.GetInt("UnlockedLevel", 1)}";
        PopupManager.Instance.ShowLoadingScene(levelName);
    }
    
    public void ShowUIHome(bool isShow = true)
    {
        _canvas.alpha = isShow ? 0 : 1;
        _canvas.blocksRaycasts = !isShow;
    }   
    
}