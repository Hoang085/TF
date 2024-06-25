using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Test3 : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        _agent.SetDestination(target.position);
    }
}