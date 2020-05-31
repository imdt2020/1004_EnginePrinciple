using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDemoRoot : MonoBehaviour
{
    public GameObject[] treePrefab;

    Vector3 Next(int n)
    {
        float g = 1.32471795724474602596f;
        float a1 = 1.0f/g;
        float a2 = 1.0f/(g*g);
        float x = Mathf.PingPong(0.5f+a1*n, 1.0f) - 0.5f;
        float z = Mathf.PingPong(0.5f+a2*n, 1.0f) - 0.5f;
        return new Vector3(x * 100, -1, z * 100);
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPoint = new Vector3(0, 0, 0);
        if (treePrefab != null && treePrefab.Length > 0)
        {
            int n = 0;
            for(int i = 0; i < 64; ++i)
            {
                Vector3 p = Next(n++);

                while((p - startPoint).sqrMagnitude < 25)
                {
                    p = Next(n++);
                }
                int t = i % treePrefab.Length;
                Instantiate(treePrefab[t], p, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
