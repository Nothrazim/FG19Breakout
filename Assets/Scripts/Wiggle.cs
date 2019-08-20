using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    public float minAngle;
    public float maxAngle;
    public float speedMultiplier;

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.PingPong(Time.time * speedMultiplier, maxAngle * 2f) + minAngle);
    }
}
