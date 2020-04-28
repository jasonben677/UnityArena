using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//AI所有的計算資料
public class AIBehaviour
{

    /// <summary>
    ///怪物的移動於轉向判斷
    /// </summary>
    /// <param name="data"></param>
    static public void Move(AIData data)
    {
        Transform m_tEnemy = data.m_ObjEnemy.transform;
        Vector3 cPos = data.m_ObjEnemy.transform.position;
        Vector3 vR = m_tEnemy.right;
        Vector3 vOriF = m_tEnemy.forward;


        m_tEnemy.forward = data.m_vCurrentVector + vR * data.m_fTempTurnForce;
        m_tEnemy.forward.Normalize();
        m_tEnemy.forward = m_tEnemy.forward;





        data.m_fSpeed = data.m_fSpeed * Time.deltaTime;
        if (data.m_fSpeed < data.m_fMinSpeed)
        {
            data.m_fSpeed = data.m_fMinSpeed;
        }
        else if (data.m_fSpeed > data.m_fMaxSpeed)
        {
            data.m_fSpeed = data.m_fMaxSpeed;
        }






        cPos = cPos + data.m_fSpeed * m_tEnemy.forward;

        m_tEnemy.position = cPos;
    }



    /// <summary>
    ///抓取目標位置並獲取轉向力
    /// </summary>
    /// <param name="data"></param>
    static public void Playerdirection(AIData data)
    {

        Vector3 cPos = data.m_ObjEnemy.transform.position;

        Vector3 vec = data.m_ArrayVTarget - cPos;

        vec.y = 0;
        vec.Normalize();

        float fDis = vec.magnitude;
        Vector3 vf = data.m_ObjEnemy.transform.forward;
        Vector3 vr = data.m_ObjEnemy.transform.right;
        data.m_vCurrentVector = vf;
        float fDotF = Vector3.Dot(vf, vec);
        float fDotR = Vector3.Dot(vr, vec);



        if (fDotF > 0.96f)
        {
            fDotF = 1.0f;
            data.m_vCurrentVector = vec;
            data.m_fTempTurnForce = 0.0f;
            fDotR = 0.0f;
        }
        else
        {
            if (fDotF < 0.0f)
            {
                if (fDotR > 0.0f)
                {
                    fDotR = 1.0f;
                }
                else
                {
                    fDotR = -1.0f;
                }
            }
            data.m_fTempTurnForce = fDotR;
        }

    }
}
