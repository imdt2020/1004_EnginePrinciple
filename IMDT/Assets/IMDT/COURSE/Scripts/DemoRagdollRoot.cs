using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRagdollRoot : MonoBehaviour
{
    public Rigidbody ragdollObject;
    public float speed = 50;
    public VariableJoystick variableJoystick;

    public void FixedUpdate()
    {
        Vector3 dir_f = Camera.main.transform.forward;
        Vector3 dir_r = Camera.main.transform.right;
        Vector3 dir_u = Camera.main.transform.up;
        Vector3 direction 
            = dir_f * variableJoystick.Vertical  * 0.25f 
            + dir_u * Mathf.Abs(variableJoystick.Vertical * 1.25f)
            + dir_r * variableJoystick.Horizontal;
        ragdollObject.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
