using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysGeomCommon : MonoBehaviour
{
    public float initialTorque = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(transform.forward * initialTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
