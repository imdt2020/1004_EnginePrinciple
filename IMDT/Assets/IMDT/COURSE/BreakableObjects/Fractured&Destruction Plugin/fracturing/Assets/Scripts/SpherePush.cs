using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePush : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidbody;

    void Start()
    {
        rigidbody.velocity = new Vector3(0, 0, 80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
