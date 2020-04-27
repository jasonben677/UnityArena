using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRay 
{


   static public void _LockTarget(AIData data) 
    {
        float Temporary = 20000f;
        Vector3 Monter = data.m_gMoster.transform.position;
        for (int i=0; i <=data.m_Target.Length; i++) 
        {
            Vector3 cpos = data.m_Target[i].transform.position - Monter;
            float vec=cpos.magnitude;
            if (vec < data.m_fPursuitRange)
            {

                if (vec < Temporary) 
                {
                    Temporary = vec;
                    int Id = i;
                    data.m_vTarget = data.m_Target[Id].transform.position;
                }


            }

            
        }
    }








    //static public void LookRange(AIData data, int x, float y, float z)
    //{
    //    GameObject vMenters = data.m_gMoster;
    //    Vector3 vLastTemp = Quaternion.Euler(0.0f, x, 0.0f) * -vMenters.transform.right;
    //    for (int i = x; i <= y; i++)
    //    {
    //        Vector3 vR = -vMenters.transform.right;
    //        Vector3 vTemp = Quaternion.Euler(0.0f, 1 * i, 0.0f) * vR;
    //        Vector3 Start = vMenters.transform.position + vLastTemp * (data.m_fProbeLenght / z);
    //        Vector3 End = vMenters.transform.position + vTemp * (data.m_fProbeLenght / z);
    //        vLastTemp = vTemp;
    //        Gizmos.DrawLine(Start, End);
    //    }


    //}

    static public bool Look(AIData data,float accuracy, float angle, float rotatePerSecond,float distance, Color debugColor)
    {
         
        float subAngle = angle / accuracy;


        for (int i = 0; i < accuracy; i++)
        {
            if (LookRay.LookAround(data, Quaternion.Euler(0, -angle / 2 + i * subAngle + Mathf.Repeat(rotatePerSecond * Time.time, subAngle), 0), distance, debugColor))
            {
                return true;
                //Debug.Log("hit Player");
            }
            
        }
        return false;
    }
   static public bool LookAround(AIData data, Quaternion eulerAnger,float distance, Color debugColor) 
    {
        GameObject gMonters = data.m_gMoster;

        Debug.DrawRay(gMonters.transform.position, eulerAnger * gMonters.transform.forward.normalized * distance, debugColor);
        Debug.DrawRay(gMonters.transform.position, gMonters.transform.forward.normalized * distance , Color.black);

        RaycastHit hit;
        if (Physics.Raycast(gMonters.transform.position, eulerAnger * gMonters.transform.forward.normalized * distance, out hit, distance) && GameObject.FindWithTag("Player")
            || Physics.Raycast(gMonters.transform.position, gMonters.transform.forward.normalized*distance, out hit, distance)&& GameObject.FindWithTag("Player")) 
        {
            Debug.Log("hit" + hit.collider.name);
            return true;
        }
        Debug.Log("hit null");
        return false;
    
    }

}
