using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    public GameObject m_Target; 
    public AIData data;

    void Start()
    {
    }

    void Update()
    {   data.m_vTarget = m_Target.transform.position;
        LookRay.Look(data, 20f, 90, 0, data.m_fPursuitRange, Color.blue);
        //LookRay.Look(data, 10, 200, 0, data.m_fPursuitRange*0.8f, Color.green);







        if ( EnterInto.EnterRange(data))
            {
                AIBehaviour.Playerdirection(data);
                AIBehaviour.Move(data);
            }
        
    }
    private void OnDrawGizmos()
    {
        if (data != null)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + transform.forward);

            Vector3 vLastTemp = Quaternion.Euler(0.0f, 30f, 0.0f) * -transform.right;

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, data.m_fPursuitRange);

            //LookRay.LookRange(data, 30, 180, 2);
        }



    }

}
