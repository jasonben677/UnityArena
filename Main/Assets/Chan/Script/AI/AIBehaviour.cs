using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour 
{
    static public void Move(Data data) 
    {
        if (data.bMove == false) 
        {
            return;
        }
    }




   static public bool seek(Data data)
    {
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0;
        float fdist = vec.magnitude;
        data.m_fTargetDistance = fdist;
        if (fdist < data.m_fSpeed+0.0001f) 
        {
            Vector3 vFinal = data.m_vTarget;
            vFinal.y = cPos.y;
            data.m_fTargetDistance = 0.0f;
            data.m_fMoveForce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_fSpeed = 0.0f;
            data.bMove = false;

            return false;
        }
        
        Vector3 vf = data.m_Go.transform.forward;
        Vector3 vr = data.m_Go.transform.right;
        data.m_fCurrentVector = vf;
        vec.Normalize();
        float fDotF = Vector3.Dot(vf, vec);
        float fDotR = Vector3.Dot(vr, vec);

        if (fDotF > 0.97f)
        {
            fDotF = 1.0f;
            data.m_fCurrentVector = vec;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else 
        {
            if (fDotF < 0.0f) 
            {
                if (fDotR > 0.0f)
                {
                    fDotR = 1.0f;
                }
                else if(fDotR<0.0f)
                {
                    fDotR = -1.0f;
                }

            }
            data.m_fTempTurnForce = fDotR;
        }

        data.m_fMoveForce = fDotR * 100f;
        data.bMove = true;
        return true;
    }


}
