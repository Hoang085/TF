using System;
using H2910.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinPopup : BasePopUp
{
    private Tween _tween;

    private void Start()
    {
        DOVirtual.DelayedCall(3f, Close);
    }

    public void Close()
    {
        OnCloseScreen();
        _tween?.Kill();
        DOVirtual.DelayedCall(0.5f, () =>
        {
            PopupManager.Instance.ShowLoadingScene("Home");
        });
        UIHomeManager.Instance.ShowUIHome(true);
    }

    public override void OnCloseScreen()
    {
        Time.timeScale = 1;
        base.OnCloseScreen();
    }
}
