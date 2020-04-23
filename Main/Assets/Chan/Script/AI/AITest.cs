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
    {






        Debug.DrawRay(this.transform.position, this.transform.position + transform.forward,Color.green,20);



        //data.m_vTarget = m_Target.transform.position;
        //if (EnterInto.EnterRange(data))
        //{


        //    AIBehaviour.Playerdirection(data);
        //    AIBehaviour.Move(data);
        //}
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


        }



    }

}
