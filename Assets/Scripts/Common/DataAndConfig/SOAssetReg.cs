using System.Collections;
using System.Collections.Generic;
using EazyEngine.Core;
using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "SO Assets Reg")]
public class SOAssetReg : GlobalConfig<SOAssetReg>
{
    [BoxGroup("Save Data")] public MainSaveDataVariable mainSaveDataVariable;

    [BoxGroup("Game Event")] public GameEvent winEvent;
    [BoxGroup("Game Event")] public GameEvent loseEvent;
    [BoxGroup("Game Event")] public GameEvent setPinEvent;
    [BoxGroup("Game Event")] public GameEvent unPinEvent;

}
