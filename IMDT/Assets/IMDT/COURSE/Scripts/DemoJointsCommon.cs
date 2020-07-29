using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoJointsCommon : MonoBehaviour
{
    public float CustomGraivtyScale = 1.0f;
    
    private Vector3 startPosition;
    private float objTime;
    public Vector3 moveDir = new Vector3(0, 0, 1);

    [HideInInspector] public Rigidbody rigidbodyInst;

    public bool useCustomGravity = true;

    void Awake()
    {
        startPosition = transform.position;
        objTime = 0;
        
        rigidbodyInst = GetComponent<Rigidbody>();
        rigidbodyInst.AddTorque(transform.forward * -3);
    }

    void FixedUpdate()
    {
        if (rigidbodyInst.isKinematic)
        {
            if (moveDir.x != 0 || moveDir.y != 0 || moveDir.z != 0)
            {
                objTime += Time.fixedDeltaTime;
                transform.position = startPosition + moveDir * Mathf.Sin(objTime);
            }
        }
        else
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
}
