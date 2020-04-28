using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    public AIData data;


    void Start()
    {
        //賦予所有怪物Layer為Enemy
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        //獲取所有Tag為Player的目標
        data.ArrTarget = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        LookRay.LockTarget(data);
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
            Gizmos.DrawLine(this.transform.position, this.transform.position + transform.forward);

            Vector3 vLastTemp = Quaternion.Euler(0.0f, 30f, 0.0f) * -transform.right;

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, data.m_fPursuitRange);


        }



    }

}
