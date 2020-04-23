using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterInto
{
   static public bool EnterRange(AIData data)
    {
        GameObject m_Moster = data.m_gMoster;
        Vector3 cpos = m_Moster.transform.position;
        Vector3 vec = data.m_vTarget - cpos;
        float m_fVec=vec.magnitude;
        vec.Normalize();
        Vector3 vf = data.m_gMoster.transform.forward;
        float fdot = Vector3.Dot(vf,vec);

        Debug.Log(fdot);
        if (data != null)
        {
            if (fdot > 0)
            {
                if (m_fVec < data.m_fPursuitRange)
                {
                    return true;
                }
            }
            else if (fdot <= 0) 
            {
                if (m_fVec < (data.m_fPursuitRange / 3)) return true;
                
                
            }
        }
        return false;

    }

}
