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
        Transform t = data.m_Go.transform;
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vR = t.right;
        Vector3 vOriF = t.forward;
        Vector3 vF = data.m_fCurrentVector;

        if (data.m_fTempTurnForce > data.m_fRot)
        {
            data.m_fTempTurnForce = data.m_fRot;
        }
        else if (data.m_fTempTurnForce < -data.m_fRot) 
        {
            data.m_fTempTurnForce = -data.m_fRot;
        }
        vF = vF + vR *data.m_fTempTurnForce;
        vF.Normalize();
        t.forward = vF;

        data.m_fSpeed = data.m_fSpeed + data.m_fMoveForce * Time.deltaTime;

        if ( data.m_fSpeed > data.m_fMaxSpeed )
        {
            data.m_fSpeed = data.m_fMaxSpeed;
        }
        else if ( data.m_fSpeed < data.m_fMinSpeed ) 
        {
            data.m_fSpeed = data.m_fMinSpeed;
        }


    }



   // public bool CheckCollision(Data data) 
   // {    }
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
