using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static Transform GetClosestObject(this Transform transform, ref List<GameObject> activeWorryObjects)
    {
        if (activeWorryObjects.Count <= 0)
        { return null; }

        Transform closestObject = null;
        float currentClosestDistance = float.MaxValue; //alternative: Mathf.Infinity
        foreach (var item in activeWorryObjects)
        {
            float distance = Vector2.Distance(transform.position, item.transform.position);
            if (distance < currentClosestDistance)
            {
                currentClosestDistance = distance;
                closestObject = item.transform;
            }
        }
        return closestObject;
    }
}
