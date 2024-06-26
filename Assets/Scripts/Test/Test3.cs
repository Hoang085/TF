using System;
using Bayat.SaveSystem;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using TF.Data;
public class Test3 : DataController
{
    void Start () {
        Load();
    }

    void OnApplicationQuit () {
        Save();
    }

    public override void Save () {
        AutoSaveManager.Current.Save();
    }

    public override void Load () {
        AutoSaveManager.Current.Load();
    }

    public override void Delete()
    {
        
    }
}