using System;
using System.Collections.Generic;
using DG.Tweening;
using H2910.UI.Popups;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InforCharPopup : BasePopUp
{
    [SerializeField] private GameEvent changeUnit;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private List<TextMeshProUGUI> attackTxts;
    [SerializeField] private List<TextMeshProUGUI> healthTxts;
    [SerializeField] private List<Image> charImages;

    [SerializeField]private Unit _unit;
    private Tween _tween;
    private bool _onClick;

    private void OnEnable()
    {
        changeUnit.AddListener(ChangeUnit);
    }

    private void OnDisable()
    {
        changeUnit.RemoveListener(ChangeUnit);
    }

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

    private void ChangeUnit()
    {
        _unit = UIHomeManager.Instance.curUnit;
        ShowProperties();
    }

    private void ShowProperties()
    {
        nameTxt.text = $"Stats\n{_unit.name}";
        
        for (int i = 0; i < attackTxts.Count; i++)
        {
            attackTxts[i].text = _unit.atk.ToString();
        }
        
        for (int i = 0; i < healthTxts.Count; i++)
        {
            healthTxts[i].text = _unit.health.ToString();
        }
        
        charImages[0].sprite = Resources.Load<Sprite>($"Sprite/{_unit.fullSprite.name}");
        charImages[1].sprite = Resources.Load<Sprite>($"Sprite/{_unit.fullSpriteEnermy.name}");
        
    }
    
}
