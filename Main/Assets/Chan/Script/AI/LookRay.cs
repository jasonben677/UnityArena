using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRay : MonoBehaviour
{










    static public void LookRange(AIData data, int x, float y, float z)
    {
        GameObject vMenters = data.m_gMoster;
        Vector3 vLastTemp = Quaternion.Euler(0.0f, x, 0.0f) * -vMenters.transform.right;
        for (int i = x; i <= y; i++)
        {
            Vector3 vR = -vMenters.transform.right;
            Vector3 vTemp = Quaternion.Euler(0.0f, 1 * i, 0.0f) * vR;
            Vector3 Start = vMenters.transform.position + vLastTemp * (data.m_fProbeLenght / z);
            Vector3 End = vMenters.transform.position + vTemp * (data.m_fProbeLenght / z);
            vLastTemp = vTemp;
            Gizmos.DrawLine(Start, End);
        }


    }
}
