using System;
using Bayat.SaveSystem;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using TF.Data;
public class Cards : DataController
{
    private CardData _cardData;
    public override void Save()
    {
        
    }

    public override void Load()
    {
        
    }

    public override void Delete()
    {
        
    }
}

[Serializable]
public class CardData
{
    private string name;
    private int lvel;
    private int progess;
}

