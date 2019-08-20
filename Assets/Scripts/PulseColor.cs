using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PulseColor : MonoBehaviour
{
    public Gradient gradient;
    public float speedMultiplier = 1f;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.color = gradient.Evaluate(Mathf.PingPong(Time.time * speedMultiplier, 1));
    }
}
