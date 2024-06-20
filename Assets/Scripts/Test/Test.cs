using System;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] float transitionDuration;

    private void Start()
    {
        StartCoroutine(LerpMovementChunk(transform.position, target.transform.position));
    }
    IEnumerator LerpMovementChunk(Vector3 localPos, Vector3 destination)
    {
        float timeElapsed = 0;
        while (timeElapsed < transitionDuration)
        {
            float t = timeElapsed / transitionDuration;

            transform.localPosition = Vector3.Lerp(localPos, destination, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = destination;
    }
}