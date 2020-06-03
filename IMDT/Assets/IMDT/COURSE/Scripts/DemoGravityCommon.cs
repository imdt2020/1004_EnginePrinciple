using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGravityCommon : MonoBehaviour
{
    public float CustomGraivtyScale = 1.0f;

    [HideInInspector] public Rigidbody rigidbodyInst;

    public bool useCustomGravity = true;

    void Awake()
    {
        rigidbodyInst = GetComponent<Rigidbody>();
        rigidbodyInst.AddTorque(transform.forward * -3);
    }

    void FixedUpdate()
    {
        if (useCustomGravity)
        {
            rigidbodyInst.useGravity = false;
            // F = ma
            rigidbodyInst.AddForce(Physics.gravity * rigidbodyInst.mass * CustomGraivtyScale);
        }
        else
        {
            rigidbodyInst.useGravity = true;
        }
    }
}
