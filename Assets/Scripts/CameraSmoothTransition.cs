using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSmoothTransition : MonoBehaviour
{
    public Transform targetTransform;
    public float transitionDuration = 2f;
    public Transform pressSpaceCanvas;

    private Transform mainCameraTransform;

    public UnityEvent afterTransition;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressSpaceCanvas.gameObject.SetActive(false);
            StartCoroutine(TransitionToTarget(() =>
            {
                afterTransition?.Invoke();
            }));
        }
    }

    private IEnumerator TransitionToTarget(Action onTransitionComplete)
    {
        float startTime = Time.time;
        Vector3 initialPosition = mainCameraTransform.position;
        Quaternion initialRotation = mainCameraTransform.rotation;

        while(Time.time - startTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;

            mainCameraTransform.position = Vector3.Lerp(initialPosition, targetTransform.position, t);
            mainCameraTransform.rotation = Quaternion.Lerp(initialRotation, targetTransform.rotation, t);

            yield return null;
        }

        onTransitionComplete?.Invoke();
    }
}
