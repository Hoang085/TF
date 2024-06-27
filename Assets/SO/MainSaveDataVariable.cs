using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class VariableSaveEvent : UnityEvent<VariableSave>
{
}

[CreateAssetMenu(
    fileName = "VariableSaveData.asset",
    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "VariableSaveData",
    order = 120)]
public class MainSaveDataVariable : BaseVariable<VariableSave,VariableSaveEvent>
{
    public override VariableSave Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            Raise();
        }
    }
}
