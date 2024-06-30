
using System;
using System.Collections.Generic;
using TF.Data;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CardPanel : PanelBase
{
    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject card;
    [SerializeField] private List<GameObject> cardSpawneds;
    
    
    private void Start()
    {
        InitCard();
    }

    private void InitCard()
    {
        for (int i = 0; i < GameData.Instance.playerData.carDatas.Count; i++)
        {
            var t = Instantiate(card, Vector3.zero, quaternion.identity, contentTransform);
            cardSpawneds.Add(t);
        }

        for (int i = 0; i < cardSpawneds.Count; i++)
        {
            cardSpawneds[i].GetComponent<BaseCard>().level = GameData.Instance.playerData.carDatas[i].level;
            cardSpawneds[i].GetComponent<BaseCard>().levelProgress = GameData.Instance.playerData.carDatas[i].progress;
        }
    }
}