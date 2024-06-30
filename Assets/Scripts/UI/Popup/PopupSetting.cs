using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using H2910.Defines;
using H2910.UI.Popups;
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
        //bgmVolume = PlayerData.Instance.PlayerSetting.BGMValue;
        //soundVolume = PlayerData.Instance.PlayerSetting.SFXValue;
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
        //PlayerData.Instance.PlayerSetting.SaveBGMValue(bgmVolume);
        //SoundManager.Instance.GlobalVMusicVolume = bgmVolume;
        BgmChange?.Invoke(bgmVolume);
    }
    public void SetSoundVolume(float value)
    {
        soundVolume = value;
        //PlayerData.Instance.PlayerSetting.SaveSFXValue(soundVolume);
        //SoundManager.Instance.GlobalSoundsVolume = soundVolume;
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