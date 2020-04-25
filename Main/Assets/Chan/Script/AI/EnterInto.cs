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
        float m_fVec = vec.magnitude;
        vec.Normalize();
        Vector3 vf = data.m_gMoster.transform.forward;
        float fdot = Vector3.Dot(vf, vec);

        if (data != null)
        {
            if (m_fVec < (data.m_fPursuitRange * 0.3f))
            {
                data.m_bChase = true;
            }
            else if (m_fVec > (data.m_fPursuitRange * 0.3f))
            {
                if (fdot > 0)
                {
                    if (m_fVec < (data.m_fPursuitRange * 0.8f))
                    {
                        data.m_bChase = true;
                    }
                    else if (LookRay.Look(data, 20f, 90, 0, data.m_fPursuitRange, Color.blue)) 
                    {
                        if (m_fVec > data.m_fPursuitRange)
                        {
                            data.m_bChase = true;
                        }
                        else 
                        {
                            data.m_bChase = false;
                        }
                    }
                }
                else if (fdot < 0) 
                {
                    data.m_bChase = false;
                }
            }
            data.m_bChase = false;

        }
        return data.m_bChase;

    }

}
