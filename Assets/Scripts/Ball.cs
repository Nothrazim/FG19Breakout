using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody2D body;
    private AudioSource hitAudio;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        hitAudio = GetComponent<AudioSource>();

        Assert.IsNotNull(body, "Failed to find rigid body 2D component");

    }

    private void Start()
    {
        GameMode.instance.OnBallAdded();
    }

    private void OnDestroy()
    {
        GameMode.instance.OnBallRemoved();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = body.velocity.normalized;
        velocity *= speed;
        body.velocity = velocity;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitAudio.isPlaying)
        {
            hitAudio.Stop();
        }
        hitAudio.Play();
    }
}
