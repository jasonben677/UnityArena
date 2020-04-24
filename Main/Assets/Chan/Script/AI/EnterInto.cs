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

        if (data != null)
        {
            if (m_fVec < (data.m_fPursuitRange / 3))
            {
                data.m_bChase = true;
                return data.m_bChase;
            }
            else
            {
                if (fdot > 0)
                {
                    if (LookRay.Look(data, 10f, 200, 200f, data.m_fPursuitRange, Color.blue))
                    {

                        data.m_bChase = true;
                        return data.m_bChase;
                    }
                }
            }
        }
        data.m_bChase = false;
        return data.m_bChase;
        
    }

}
