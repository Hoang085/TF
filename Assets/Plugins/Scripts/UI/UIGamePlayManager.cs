using DG.Tweening;
using H2910.Common.Singleton;
using H2910.UI.Popups;
using H2910.Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;

public class UIGamePlayManager : ManualSingletonMono<UIGamePlayManager>
{
    [SerializeField] private GameEvent winEvent;
    [SerializeField] private GameEvent loseEvent;
    [SerializeField] private GameEvent isFullCrewEvent;
    [SerializeField] private GameEvent bonusTimeEvent;

    [SerializeField] private GameObject noticeText;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private float timeLeft;

    private bool _timerOn = false;
    private Tween _tween;
    private bool _onClick;

    private void OnEnable()
    {
        winEvent.AddListener(WinGame);
        loseEvent.AddListener(LoseGame);
        isFullCrewEvent.AddListener(ShowNotice);
        bonusTimeEvent.AddListener(BonusTime);
    }

    private void OnDisable()
    {
        winEvent.RemoveListener(WinGame);
        loseEvent.RemoveListener(LoseGame);
        isFullCrewEvent.RemoveListener(ShowNotice);
        bonusTimeEvent.RemoveListener(BonusTime);
    }

    private void Start()
    {
        _timerOn = true;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                loseEvent.Raise();
                _timerOn = false;
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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

    public void OnBtnPauseClick()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        Time.timeScale = 0;
        PopupManager.Instance.OnShowScreen(PopupName.Pause, ParentPopup.Hight);
    }

    private void WinGame()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        PopupManager.Instance.OnShowScreen(PopupName.Win, ParentPopup.Hight);
    }

    private void LoseGame()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        PopupManager.Instance.OnShowScreen(PopupName.Lose, ParentPopup.Hight);
    }

   private void ShowNotice()
    {
        noticeText.SetActive(true);
        DOVirtual.DelayedCall(1f, () => noticeText.SetActive(false));
    }

    private void BonusTime()
    {
        timeLeft += 60f;
        _timerOn = true;
    }
}
