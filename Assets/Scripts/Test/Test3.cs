using System;
using UnityEngine;

public class Test_3 : MonoBehaviour
{
    [SerializeField]private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("A");
            _animator.SetTrigger("Attack");
        }
    }
}
