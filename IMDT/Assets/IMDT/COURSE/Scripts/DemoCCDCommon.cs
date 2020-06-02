using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCCDCommon : MonoBehaviour
{
    public float initialSpeed = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir_f = -transform.right;
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(dir_f * initialSpeed);
        rb.AddTorque(transform.forward * 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
