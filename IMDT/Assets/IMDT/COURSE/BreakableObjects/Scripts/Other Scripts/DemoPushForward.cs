using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPushForward : MonoBehaviour
{
    public float force = 10f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }
}
