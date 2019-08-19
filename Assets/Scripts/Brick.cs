using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Camera))]

public class Brick : MonoBehaviour
{
    [Tooltip("Should we cause the camera to shake if the ball hits this brick?")]
    public bool causeCameraShake = false;
    public bool isBreakable = true;

    [Tooltip("Number of sprites = number of hits the brick can take")]
    public List<Sprite> sprites = new List<Sprite>();
    public int health = 3;

    private SpriteRenderer spriteRender;


    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRender, "Failed to find sprite renderer components.");
    }

    private void Start()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        var rend = go.GetComponent<Renderer>();
        rend.material.mainTexture = Resources.Load("brick_broken") as Texture;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (causeCameraShake)
        {
            GameCamera.instance.cameraShake.Shake();
        }

        if (!isBreakable)
        { return; }

        if (health > 0)
        {
            health -= 1;
            //todo: create transparency in sprites
            // make an overlay (texture?) of broken brick that becomes gradually less transparent and tehn destroy brick (as below)
            //replace sprite with damaged sprite
            //spriteRender.sprite = sprites[0];
        }
        else
        {
            Destroy(gameObject);
        }
    }
}