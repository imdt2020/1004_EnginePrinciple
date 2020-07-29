using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoJointsRoot : MonoBehaviour
{
    public float gForce = -10.0f;
    
    void Start()
    {
        Physics.gravity = new Vector3(0, gForce, 0);
    }
}
