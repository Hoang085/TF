using DG.Tweening;
using H2910.Common.Singleton;
using H2910.UI.Popups;
using H2910.Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGamePlayManager : ManualSingletonMono<UIGamePlayManager>
{
    [SerializeField] private TextMeshProUGUI noticeText;
    [SerializeField] private TextMeshProUGUI foodAmountTxt;
    [SerializeField] private Image coolDownImage;

    public float foodAmount;
    
    private bool _isCoolingDown;
    private float _coolDownTime = 5f;
    
    private Tween _tween;
    private bool _onClick;
    
    public override void Awake()
    {
        base.Awake();
        foodAmount = 0;
        foodAmountTxt.text = foodAmount.ToString();
        
        _isCoolingDown = true;
        coolDownImage.fillAmount = 0;
        StartCoroutine(StartCoolDown());
    }
    
    IEnumerator StartCoolDown()
    {
        if(!_isCoolingDown) yield break;

        while (coolDownImage.fillAmount < 1)
        {
            coolDownImage.fillAmount += 1.0f / _coolDownTime * Time.deltaTime;

            yield return null;
        }

        if (coolDownImage.fillAmount >= 1)
        {
            foodAmount += 1;
            foodAmountTxt.text = foodAmount.ToString();
            coolDownImage.fillAmount = 0;
            StartCoroutine(StartCoolDown());
        }
    }

    private void CheckPrice()
    {
        
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
        noticeText.gameObject.SetActive(true);
        DOVirtual.DelayedCall(1f, () => noticeText.gameObject.SetActive(false));
    }
   
}
