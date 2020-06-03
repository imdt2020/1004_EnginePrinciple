using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPhysGeomCommon : MonoBehaviour
{
    public float initialTorque = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (DemoPhysGeomRoot.ApplyTorque)
            GetComponent<Rigidbody>().AddTorque(transform.forward * initialTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
