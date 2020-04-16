using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public enum eState 
    {
    NONE=-1,
    OUTSIDE_TEST,
    INSIDE_TEST,
    COL_TEST
    }

    public float fRadius;
    [HideInInspector]
    public eState m_State = eState.NONE;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        if (m_State == eState.INSIDE_TEST)
        {
            Gizmos.color = Color.yellow;
        }
        else if (m_State == eState.COL_TEST) 
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(this.transform.position, fRadius);


    }
}
