using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoKinematicRoot : MonoBehaviour
{
    public GameObject kinematicObject;
    public float speed = 10;
    public VariableJoystick variableJoystick;

    public void FixedUpdate()
    {
        Vector3 dir_f = Camera.main.transform.forward;
        Vector3 dir_r = Camera.main.transform.right;

        dir_f.y = 0;
        dir_f = dir_f.normalized;
        dir_r.y = 0;
        dir_r = dir_r.normalized;

        Vector3 direction 
            = dir_f * variableJoystick.Vertical 
            + dir_r * variableJoystick.Horizontal;
        kinematicObject.transform.position += (direction * speed * Time.fixedDeltaTime);
    }

    public float gForce = -5.0f;
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
