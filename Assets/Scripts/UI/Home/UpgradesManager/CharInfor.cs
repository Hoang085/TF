using System;
using System.Collections.Generic;
using DG.Tweening;
using H2910.Defines;
using H2910.UI.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharInfor : MonoBehaviour
{
    [SerializeField] private string nameChar;
    [SerializeField] private Image charImage;

    [SerializeField] private List<TextMeshProUGUI> _listTxt;
    
    private Unit _unit;
    private bool _onClick;
    
    
    private void Awake()
    {
        _listTxt.AddRange(gameObject.GetComponentsInChildren<TextMeshProUGUI>());
        _unit = Resources.Load<Unit>($"Scriptable Objects/Unit Data/Stone Age/{nameChar}");

        for (int i = 0; i < _listTxt.Count; i++)
        {
            switch (i)
            {
                case 0:
                    _listTxt[i].text = nameChar;
                    break;
                case 1:
                    _listTxt[i].text = _unit.atk.ToString();
                    break;
                case 2:
                    _listTxt[i].text = _unit.health.ToString();
                    break;
                default:
                    break;
            }
        }
        
        charImage.sprite = Resources.Load<Sprite>($"Sprite/{_unit.fullSprite.name}");
    }
    
    private void BlockMultyClick()
    {
        _onClick = true;
        DOVirtual.DelayedCall(0.1f, () => { _onClick = false; });
    }   
    
    public void OnBtnInforChar()
    {
        if (_onClick)
            return;
        BlockMultyClick();
        PopupManager.Instance.OnShowScreen(PopupName.InforChar, ParentPopup.Hight);
    }
}
