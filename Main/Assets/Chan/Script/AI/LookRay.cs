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
            float m = Start.magnitude;
     
            Vector3 End = vMenters.transform.position + vTemp * (data.m_fProbeLenght / z);
            vLastTemp = vTemp;
            Gizmos.DrawLine(Start, End);
        }


    }
  
    static public bool Look(AIData data,float accuracy, float angle, float rotatePerSecond,float distance, Color debugColor)
    {
         
        float subAngle = angle / accuracy;


        for (int i = 0; i < accuracy; i++)
        {
            if (LookRay.LookAround(data, Quaternion.Euler(0, -angle / 2 + i * subAngle + Mathf.Repeat(rotatePerSecond * Time.time, subAngle), 0), distance, debugColor)) 
            {
                return true;
            }
            
        }
        return true;
    }
   static public bool LookAround(AIData data, Quaternion eulerAnger,float distance, Color debugColor) 
    {
        GameObject gMonters = data.m_gMoster;

        Debug.DrawRay(gMonters.transform.position, eulerAnger * gMonters.transform.forward.normalized * distance, debugColor);

        RaycastHit hit;
        if (Physics.Raycast(gMonters.transform.position, eulerAnger * gMonters.transform.forward.normalized * distance, out hit, distance) && hit.collider.name=="Player") 
        {
            data.m_vTarget = hit.transform.position;
            return true;
        }
        Debug.Log("hit null");
        return false;
    }

}
