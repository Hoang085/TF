using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] internal BoolGameEvent onGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        onGameOver.AddListener(GameOverEvent);
    }

    private void GameOverEvent(bool isGameOver)
    {
        if (isGameOver)
        {
            Debug.Log("Game Over");
            onGameOver.RemoveListener(GameOverEvent);
        }
    }
}
