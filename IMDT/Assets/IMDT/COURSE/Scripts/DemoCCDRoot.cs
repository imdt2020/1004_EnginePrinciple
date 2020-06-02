using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCCDRoot : MonoBehaviour
{
    public float gForce = -3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
