using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    public AIData data;

    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        data.m_Target = GameObject.FindGameObjectsWithTag("Player");

    }

    void Update()
    {

        data.m_vTarget = data.m_Target[0].transform.position;

        EnterInto.EnterRange(data);

        if (data.m_bChase)
        {
            data.m_fRotatePerSecond = 0f;
            AIBehaviour.Playerdirection(data);
            AIBehaviour.Move(data);
        }
        else
        {
            data.m_fRotatePerSecond = 100f;
        }
    }
    private void OnDrawGizmos()
    {
        if (data != null)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + transform.forward*20);

            Vector3 vLastTemp = Quaternion.Euler(0.0f, 30f, 0.0f) * -transform.right;

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, data.m_fPursuitRange);


        }



    }

}
