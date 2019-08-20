using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(CircleCollider2D))]
public class Star : MonoBehaviour
{
    [SerializeField] private Sprite normalMouth = null;
    [SerializeField] private Sprite worriedMouth = null;
    public LayerMask reactToLayer;

    private CircleCollider2D awareness;
    private SpriteRenderer mouth;
    private LookAtTarget leftEye;
    private LookAtTarget rightEye;

    private List<GameObject> activeWorryObjects = new List<GameObject>();

    private void Awake()
    {
        awareness = GetComponent<CircleCollider2D>();

        Transform go = transform.Find("LeftEye/Pupil");
        Assert.IsNotNull(go, "Failed to locate child \"LeftEye/Pupil\".");
        leftEye = go.GetComponent<LookAtTarget>();
        Assert.IsNotNull(leftEye, "Failed to locate LookAtMouse component.");

        go = transform.Find("RightEye/Pupil");
        Assert.IsNotNull(go, "Failed to locate child \"RightEye/Pupil\".");
        rightEye = go.GetComponent<LookAtTarget>();
        Assert.IsNotNull(rightEye, "Failed to locate LookAtMouse component.");

        go = transform.Find("Mouth");
        Assert.IsNotNull(go, "Failed to locate child \"Mouth\".");
        mouth = go.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(mouth, "Failed to locate SpriteRenderer Mouth component.");
    }

    private void Start()
    {
        GameMode.instance.OnStarsAdded();
    }

    private void OnDestroy()
    {
        GameMode.instance.OnStarsRemoved();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activeWorryObjects.Add(collision.gameObject);
        UpdateTarget();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeWorryObjects.Remove(collision.gameObject);
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        if (activeWorryObjects.Count > 0)
        {
            Transform target = transform.GetClosestObject(ref activeWorryObjects); 
            SetWorried(target);
        }
        else
        {
            SetNormal();
        }
    }

    public void SetWorried(Transform target)
    {
        mouth.sprite = worriedMouth;
        leftEye.target = target;
        rightEye.target = target;
    }

    public void SetNormal()
    {
        mouth.sprite = normalMouth;
        leftEye.target = null;
        rightEye.target = null;
        leftEye.transform.localPosition = Vector3.zero;
        rightEye.transform.localPosition = Vector3.zero;
    }
}
