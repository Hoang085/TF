using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using H2910.Common.Singleton;
using H2910.Defines;
using H2910.UI.Popups;
using UnityEngine;

public class GameManager : ManualSingletonMono<GameManager>
{
    [SerializeField] internal BoolGameEvent onGameOver;
    
    void OnEnable()
    {
        onGameOver.AddListener(GameOverEvent);
    }

    private void GameOverEvent(bool isGameOver)
    {
        if (isGameOver)
        {
            onGameOver.RemoveListener(GameOverEvent);
            PopupManager.Instance.OnShowScreen(PopupName.Lose,ParentPopup.Hight);
        }
    }
}
