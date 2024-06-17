using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameEvent moveEvent;

    private void OnEnable()
    {
        moveEvent.AddListener(Dosmt);
    }

    private void OnDisable()
    {
        moveEvent.RemoveListener(Dosmt);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveEvent.Raise();
        }
    }

    private void Dosmt()
    {
        print("a");
    }
}
