using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using H2910.Common.Singleton;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

    public class CooldDown : MonoBehaviour
    {
        [SerializeField] private Image coolDownImage;
        private bool _isCoolingDown;
        private float _coolDownTime = 5f;
        
        private void Awake()
        {
            _isCoolingDown = true;
            coolDownImage.fillAmount = 0;
            StartCoroutine(StartCoolDown());
        }

        IEnumerator StartCoolDown()
        {
            if(!_isCoolingDown) yield break;

            while (coolDownImage.fillAmount < 1)
            {
                coolDownImage.fillAmount += 1.0f / _coolDownTime * Time.deltaTime;

                yield return null;
            }

            if (coolDownImage.fillAmount >= 1)
            {
                coolDownImage.fillAmount = 0;
                StartCoroutine(StartCoolDown());
            }
        }
    }

