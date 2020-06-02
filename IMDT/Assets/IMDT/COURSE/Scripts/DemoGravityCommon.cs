using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGravityCommon : MonoBehaviour
{
    public float CustomGraivtyScale = 1.0f;
    [HideInInspector] public Rigidbody rigidbody;

    public bool useCustomGravity = true;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddTorque(transform.forward * -3);
    }

    void FixedUpdate()
    {
        if (useCustomGravity)
        {
            rigidbody.useGravity = false;
            rigidbody.AddForce(Physics.gravity * (rigidbody.mass * rigidbody.mass) * CustomGraivtyScale);
        }
        else
        {
            rigidbody.useGravity = true;
            
        }
    }
}
