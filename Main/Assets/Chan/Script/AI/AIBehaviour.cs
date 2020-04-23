using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour 
{

    static public void Move(AIData data) 
    {
        Transform t= data.m_gMoster.transform;
        Vector3 cPos = data.m_gMoster.transform.position;
        Vector3 vR = t.right;
        Vector3 vOriF = t.forward;


        t.forward = data.m_vCurrentVector + vR * data.m_fTempTurnForce;
        t.forward.Normalize();
        t.forward = t.forward;





        data.m_fSpeed = data.m_fSpeed*Time.deltaTime;
        if (data.m_fSpeed < data.m_fMinSpeed)
        {
            data.m_fSpeed = data.m_fMinSpeed;
        }
        else if (data.m_fSpeed > data.m_fMaxSpeed) 
        {
            data.m_fSpeed = data.m_fMaxSpeed;
        }






        cPos = cPos + data.m_fSpeed * t.forward;

        t.position = cPos;
    }



   

    static public void Playerdirection(AIData data)
    {

        Vector3 cPos = data.m_gMoster.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0;
        vec.Normalize();

        float fDis = vec.magnitude;
        Vector3 vf = data.m_gMoster.transform.forward;
        Vector3 vr = data.m_gMoster.transform.right;
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
