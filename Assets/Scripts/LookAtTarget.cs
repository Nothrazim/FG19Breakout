using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public float factor = 0.25f;
    public float limit = 0.08f;
    [System.NonSerialized] public Transform target;
    private Vector3 center;

    private void Start()
    {
        center = transform.position;
    }

    private void Update()
    {
        if (!target)
        { return; }

        Vector3 targetPosition = target.position;
        targetPosition.z = 0f;
        Vector3 direction = (targetPosition - transform.position) * factor;
        direction = Vector3.ClampMagnitude(direction, limit);
        transform.position = center + direction;
    }
}
