using System;
using System.Collections;
using DG.Tweening;
using H2910.Defines;
using H2910.UI.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingTxt;
    [SerializeField] private float loadingTime;

    private int _count = 0;
    private float _timeLoading = 2;

    private void Awake()
    {
        LoadSceneAsync("Home",LoadingBgType.MineralMine);
    }

    IEnumerator LoadAsyncScene(string mapName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mapName);
        asyncLoad.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(_timeLoading);
        asyncLoad.allowSceneActivation = true;  
        yield return new WaitForSecondsRealtime(0.05f);
        gameObject.SetActive(false);
    }

    public void LoadSceneAsync(string mapName, LoadingBgType loadingBgType)
    {
        switch (loadingBgType)
        {
            case LoadingBgType.None:
                _timeLoading = 2f;
                break;
            default:
                break;
        }
        _count = 0;
        StopAllCoroutines();
        StartCoroutine(LoadAsyncScene(mapName));
    }

    private void OnEnable()
    {
        if(loadingTxt == null)
            return;
        UpdateText();
    }

    private void UpdateText()
    {
        loadingTxt.text = "Loading";
        AddDot();
        DOVirtual.DelayedCall(0.5f, () =>
        {
            if (gameObject != null)
                AddDot();
        }).SetLoops(int.MaxValue).SetUpdate(true);
    }

    private void AddDot()
    {
        loadingTxt.text += ".";
        _count += 1;
        if (_count == 4)
        {
            loadingTxt.text = "Loading";
            _count = 0;
        }
    }    
    
}