using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Flip : MonoBehaviour
{
    public float flipperSpeed = 1000f;
    public float reverseMod = 1f;

    [System.NonSerialized] public bool isPressed = false;

    private HingeJoint2D hinge;
    private void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
        Assert.IsNotNull(hinge, "Could not find hinge component");
    }

    private void FixedUpdate()
    {
        if (isPressed)
        {
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = reverseMod * flipperSpeed;
            hinge.motor = motor;
        }
        else
        {
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = reverseMod * -flipperSpeed;
            hinge.motor = motor;
        }
    }
}
