using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using H2910.Defines;
using H2910.UI.Popups;
using TF.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : BasePopUp
{
    [SerializeField] private string privacyURL;
    [SerializeField] private string rateUsURL;
    [SerializeField] private ButtonSetting[] buttonSettings;
    [SerializeField] private SliderSetting[] sliderSettings;

    public event Action<float> BgmChange;
    public event Action<float> SoundChange;
    
    private float bgmVolume;
    private float soundVolume;
    private Tween tween;

    public override void OnShowScreen()
    {
        base.OnShowScreen();
        bgmVolume = GameData.Instance.playerData.playerSetting.BGMValue;
        soundVolume = GameData.Instance.playerData.playerSetting.soundValue;
        foreach (var bt in buttonSettings)
        {
            bt.Init(this);
        }
        foreach (var sl in sliderSettings)
        {
            sl.Init(this);
        }
        SetBgmVolume(bgmVolume);
        SetSoundVolume(soundVolume);
        tween?.Kill();
    }
    
    public void SetBgmVolume(float value)
    {
        bgmVolume = value;
        GameData.Instance.playerData.playerSetting.BGMValue = bgmVolume;
        SoundManager.Instance.GlobalMusicVolume = bgmVolume;
        BgmChange?.Invoke(bgmVolume);
    }
    
    public void SetSoundVolume(float value)
    {
        soundVolume = value;
        GameData.Instance.playerData.playerSetting.soundValue = soundVolume;
        SoundManager.Instance.GlobalSoundsVolume = soundVolume;
        SoundChange?.Invoke(soundVolume);
    }

    public void SetVibra()
    {
        //SetVibra
    }
    
    public void PrivacyPolicy()
    {
        if (OnClick)
            return;
        BlockMultyClick();
        Application.OpenURL(privacyURL);
    }
    
    public void Close()
    {
        if (OnClick)
            return;
        BlockMultyClick();
        OnCloseScreen();
        tween?.Kill();
    }
    
    public override void OnCloseScreen()
    {
        base.OnCloseScreen();
    }

}