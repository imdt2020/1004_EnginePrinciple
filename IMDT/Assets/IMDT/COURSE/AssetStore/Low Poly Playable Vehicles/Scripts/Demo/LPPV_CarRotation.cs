using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPPV_CarRotation : MonoBehaviour {

	private float targetAngle;
	private float vel;
	private float smoothSpeed = 10f;

	private void Start () 
	{
		if (transform.localEulerAngles.y > 0) 
		{
			targetAngle = 25f + transform.localEulerAngles.y;
		}else
		{
			targetAngle = -25f - transform.localEulerAngles.y;
		}
	}

	private void FixedUpdate () 
	{
		Quaternion target = Quaternion.Euler (new Vector3(transform.localEulerAngles.x, targetAngle, transform.localEulerAngles.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smoothSpeed);
		if (targetAngle > 0)
			targetAngle++;
		else
			targetAngle--;
	}
}
