using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICMain : MonoBehaviour
{
    public GameObject m_gTarget;
    public Data m_Data;
   
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        m_Data.m_vTarget = m_gTarget.transform.position;
        AIBehaviour.seek(m_Data);

        AIBehaviour.Move(m_Data);
    }


    private void OnDrawGizmos()
    {
       





        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position + transform.forward);
        //Gizmos.DrawWireSphere(this.transform.position, m_Data.m_fRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, m_Data.m_fProbeLength);


       
    }
}
