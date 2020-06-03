using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoKinematicPlatform : MonoBehaviour
{
    private Vector3 startPosition;
    private float objTime;
    public Vector3 moveDir = new Vector3(1, 0, 0);
    public float moveExtends = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        objTime = 0;
    }

    public void FixedUpdate()
    {
        objTime += Time.fixedDeltaTime;

        transform.position = startPosition + moveDir * Mathf.Sin(objTime) * moveExtends;
    }
}
