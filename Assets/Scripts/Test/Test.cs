using System;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayers;

    private float _distance = 1f;
    private float _radius = 0.05f;
    private GameObject _target;
    private float _curHitDistance;
    private Vector3 rayOrigin;
    private Vector3 direction;

    private void Awake()
    {
        rayOrigin = gameObject.transform.position;
        direction = gameObject.transform.forward;
    }

    private void CheckForObstacles()
    {
        RaycastHit hit;

        if (Physics.SphereCast(rayOrigin, _radius, direction, out hit, _distance, obstacleLayers, QueryTriggerInteraction.UseGlobal))
        {
            _target = hit.transform.gameObject;
            _curHitDistance = hit.distance;
        }
        else
        {
            _curHitDistance = _distance;
            _target = null;
        }

    }
}