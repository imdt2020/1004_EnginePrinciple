using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityChan;
public class DebugDrawBone : MonoBehaviour
{
    public DebugDrawBone( )
    {
        
    }
        

    void Start ()
    {
       
    }

    void Update ()
    {



    }
    public bool drawcoordinate = true;
    public float coorsize = 0.04f;

    void drawbone(Transform t, int flag) // flag just for optimization, prevent color change
    {
        //Vector3 VX = /*flag == 0 ? t.rotation * Vector3.right :*/ Vector3.right;

        foreach ( Transform child in t)
        {
            Vector3 loxalX = new Vector3(coorsize, 0,0);
            Vector3 loxalY = new Vector3(0, coorsize, 0);
            Vector3 loxalZ = new Vector3(0,0, coorsize);
            loxalX = child.rotation * loxalX;
            loxalY = child.rotation * loxalY;
            loxalZ = child.rotation * loxalZ;

            if(flag == 0)
            {
                //Debug.DrawLine(t.position * 0.1f + child.position * 0.9f, t.position * 0.9f + child.position * 0.1f, Color.white);
                Vector3 VDelta = child.position - t.position;
                float fLength = VDelta.magnitude;
                Vector3 Current = t.position + VDelta * 0.08f;
                Vector3 Dir1 = Vector3.Cross(VDelta, Vector3.right).normalized * (fLength * 0.05f);
                Vector3 Dir2 = Vector3.Cross(Dir1, VDelta).normalized * (fLength * 0.05f);

                Vector3 V1 = Current + Dir1;
                Vector3 V2 = Current - Dir1;
                Vector3 V3 = Current + Dir2;
                Vector3 V4 = Current - Dir2;

                Color drawColor = Color.white;

                Debug.DrawLine(t.position, V1, drawColor);
                Debug.DrawLine(t.position, V2, drawColor);
                Debug.DrawLine(t.position, V3, drawColor);
                Debug.DrawLine(t.position, V4, drawColor);

                Debug.DrawLine(child.position, V1, drawColor);
                Debug.DrawLine(child.position, V2, drawColor);
                Debug.DrawLine(child.position, V3, drawColor);
                Debug.DrawLine(child.position, V4, drawColor);

                Debug.DrawLine(V1, V3, drawColor);
                Debug.DrawLine(V1, V4, drawColor);
                Debug.DrawLine(V2, V3, drawColor);
                Debug.DrawLine(V2, V4, drawColor);



            }
            else if (flag == 1)
                Debug.DrawLine(child.position,  child.position+loxalX, Color.red);
            else if(flag == 2)
                Debug.DrawLine(child.position,  child.position+loxalY, Color.green);
            else
                Debug.DrawLine(child.position,  child.position+loxalZ, Color.blue);
            drawbone(child, flag);
        }
    }

    void DrawCurrent( Transform Trans )
    {
        if( Trans.parent != null )
        {
            Debug.DrawLine(Trans.position,  Trans.parent.position, Color.blue);
        }
        else
        {
            Debug.DrawLine(Trans.position,  new Vector3(0,0,0), Color.blue);
        }


        float len = 0.05f;
        Vector3 loxalX = new Vector3(len,0,0);
        Vector3 loxalY = new Vector3(0,len,0);
        Vector3 loxalZ = new Vector3(0,0,len);
        //loxalX = child.rotation * loxalX;
        //loxalY = child.rotation * loxalY;
        //loxalZ = child.rotation * loxalZ;

        //Debug.DrawLine(t.position * 0.1f + child.position * 0.9f,  t.position * 0.9f + child.position * 0.1f, Color.white);

        //Debug.DrawLine(child.position,  child.position+loxalX, Color.red);
        //Debug.DrawLine(child.position,  child.position+loxalY, Color.green);
        //Debug.DrawLine(child.position,  child.position+loxalZ, Color.blue);
    }

    void drawbone1(Transform t, int flag) // flag just for optimization, prevent color change
    {
        //Vector3 VX = /*flag == 0 ? t.rotation * Vector3.right :*/ Vector3.right;

        foreach (Transform child in t)
        {
            Vector3 loxalX = new Vector3(coorsize, 0, 0);
            Vector3 loxalY = new Vector3(0, coorsize, 0);
            Vector3 loxalZ = new Vector3(0, 0, coorsize);
            loxalX = child.rotation * loxalX;
            loxalY = child.rotation * loxalY;
            loxalZ = child.rotation * loxalZ;

            if (flag == 0)
            {
                //Debug.DrawLine(t.position * 0.1f + child.position * 0.9f, t.position * 0.9f + child.position * 0.1f, Color.white);
                Vector3 VDelta = child.position - t.position;
                float fLength = VDelta.magnitude;
                Vector3 Current = t.position + VDelta * 0.08f;
                Vector3 Dir1 = Vector3.Cross(VDelta, Vector3.right).normalized * (fLength * 0.05f);
                Vector3 Dir2 = Vector3.Cross(Dir1, VDelta).normalized * (fLength * 0.05f);

                Vector3 V1 = Current + Dir1;
                Vector3 V2 = Current - Dir1;
                Vector3 V3 = Current + Dir2;
                Vector3 V4 = Current - Dir2;

                Color drawColor = Color.red;

                Debug.DrawLine(t.position, V1, drawColor);
                Debug.DrawLine(t.position, V2, drawColor);
                Debug.DrawLine(t.position, V3, drawColor);
                Debug.DrawLine(t.position, V4, drawColor);

                Debug.DrawLine(child.position, V1, drawColor);
                Debug.DrawLine(child.position, V2, drawColor);
                Debug.DrawLine(child.position, V3, drawColor);
                Debug.DrawLine(child.position, V4, drawColor);

                Debug.DrawLine(V1, V3, drawColor);
                Debug.DrawLine(V1, V4, drawColor);
                Debug.DrawLine(V2, V3, drawColor);
                Debug.DrawLine(V2, V4, drawColor);



            }
            else if (flag == 1)
                Debug.DrawLine(child.position, child.position + loxalX, Color.red);
            else if (flag == 2)
                Debug.DrawLine(child.position, child.position + loxalY, Color.green);
            else
                Debug.DrawLine(child.position, child.position + loxalZ, Color.blue);
            drawbone(child, flag);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        drawbone(transform, 0);
        if(drawcoordinate)
        {
            drawbone(transform, 1);
            drawbone(transform, 2);
            drawbone(transform, 3);
        }

        SpringManager sm = GetComponent<SpringManager>();
        int boneCounts = sm.springBones.Length;

        for (int i = 0; i < boneCounts; ++i) {
            drawbone1(sm.springBones[i].transform, 0);
        }

        //Debug.Log(     CreateSomeClass( ) );
        return;
        //Physics2D.ci

        SkinnedMeshRenderer TRender = GetComponent<SkinnedMeshRenderer>( );
        if( TRender != null && TRender.bones != null )
        {
            Transform[] M4Array = TRender.bones;

            if( M4Array != null )
            {
                for( int nLoop =0; nLoop < M4Array.Length; ++nLoop )
                {
                    //TRender.bon

                    DrawCurrent( M4Array[nLoop] );

                //  Gizmos.DrawSphere( M4Array[nLoop].MultiplyPoint3x4( new Vector3(0,0,0) ), 0.1f );
                    //Gizmos.DrawLine(TRender.bones[nLoop].position, new Vector3(0,0,0));
                }
            }

        }
        Animator T = gameObject.GetComponent<Animator>( );


        for( int nLoop =0; nLoop < 49; ++nLoop )
        {
            //Gizmos.DrawSphere( T., 0.1f );

        }

        //TRender.sharedMesh.bindposes;


    //  drawbone(transform);
        //Animation
        //T.GetBoneTransform( );
        //Avatar;
        //SkeletonBone

     //   Gizmos.DrawLine(VLIn, VLOut);
      //  Gizmos.DrawLine(VRIn, VROut);


    }


}
