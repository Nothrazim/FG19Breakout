using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 1f;
    public float shakeMagnitude = 1f;

    private Vector3 originalPosition;
    private Coroutine shakeRoutine;

    // Property
    public bool IsShaking
    {
        get { return shakeRoutine == null ? false : true; } // if == null is true, return false (? is if true). (: is else) return true
    }

    public void Shake()
    {
        originalPosition = transform.position;
        shakeRoutine = StartCoroutine(PerformShake());
    }

    public void StopShake()
    {
        if (shakeRoutine!= null)
        {
            StopCoroutine(shakeRoutine);
        }
    }

    private IEnumerator PerformShake()
    {
        transform.position = originalPosition;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(x, y, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = originalPosition;
    }
}