using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capcol;
    public float offset = 0.3f;

    private Vector3 point1; //第一個球心
    private Vector3 point2; //第二個球心
    private float radius;

    // Start is called before the first frame update
    void Awake()
    {
        radius = capcol.radius-0.05f;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        point1 = capcol.transform.position + transform.up * (radius-offset); //讓碰撞的capsule稍微沉降一些
        point2 = capcol.transform.position + transform.up * (capcol.height-offset) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Terrain")); 

        if (outputCols.Length != 0)
        {
            //foreach (var col in outputCols)
            //{
            //    Debug.Log("collision:" + col.name);
            //}
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }
}
