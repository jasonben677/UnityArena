using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//怪物的視野範圍
public class LookRay : MonoBehaviour
{

    /// <summary>
    ///鎖定最近距離的玩家
    /// </summary>
    static public void LockTarget(AIData data)
    {
        //給個最大長度之後最近的玩家距離取代掉
        float TemLength = 2000f;
        //1.抓取怪物位置
        GameObject ObjEnemy = data.m_ObjEnemy;
        Vector3 cPos = ObjEnemy.transform.position;
        //2.抓取所有目標
        for (int i = 0; i < data.ArrTarget.Length; i++)
        {
            GameObject ObjTarget = data.ArrTarget[i];
            Vector3 vTarget = ObjTarget.transform.position;
            //3.獲取所有目標的距離 目標座標-怪物座標後轉成長度
            Vector3 vec = vTarget - cPos;
            float fDis = vec.magnitude;
            //判斷是否為最近玩家
            if (fDis <= TemLength)
            {
                TemLength = fDis;
                //儲存現在的i值
                int id = i;
                //儲存現在的目標的位置
                data.m_ArrayVTarget = data.ArrTarget[id].transform.position;
            }
        }

    }

    /*// Gizmos 的怪物追擊範圍
    static public void LookRange(AIData data, int x, float y, float z)
    {
        GameObject ObjEnemy = data.m_ObjEnemy;
        Vector3 vLastTemp = Quaternion.Euler(0.0f, x, 0.0f) * -ObjEnemy.transform.right;
        for (int i = x; i <= y; i++)
        {
            Vector3 vR = -ObjEnemy.transform.right;
            Vector3 vTemp = Quaternion.Euler(0.0f, 1 * i, 0.0f) * vR;
            Vector3 Start = ObjEnemy.transform.position + vLastTemp * (data.m_fProbeLenght / z);
            Vector3 End = ObjEnemy.transform.position + vTemp * (data.m_fProbeLenght / z);
            vLastTemp = vTemp;
            Gizmos.DrawLine(Start, End);
        }


    }*/

    /// <summary>
    /// 射線的的範圍也是視線的範圍
    /// </summary>
    /// <param name="data"></param>
    /// <param name="accuracy">Ray數量</param>
    /// <param name="angle">角度</param>
    /// <param name="rotatePerSecond">掃描速度(data.m_fRotatePerSecond)</param>
    /// <param name="distance">距離</param>
    /// <param name="debugColor">color</param>
    /// <returns></returns>
    static public bool Look(AIData data, float accuracy, float angle, float rotatePerSecond, float distance, Color debugColor)
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
    /// <summary>
    /// 打出射線確認是否打到玩家
    /// </summary>
    /// <param name="data"></param>
    /// <param name="eulerAnger">角度</param>
    /// <param name="distance">距離</param>
    /// <param name="debugColor">顏色</param>
    /// <returns></returns>
    static public bool LookAround(AIData data, Quaternion eulerAnger, float distance, Color debugColor)
    {
        GameObject ObjEnemy = data.m_ObjEnemy;
        //劃出所有掃描線
        Debug.DrawRay(ObjEnemy.transform.position, eulerAnger * ObjEnemy.transform.forward.normalized * distance, debugColor);
        //劃出正前方的射線
        Debug.DrawRay(ObjEnemy.transform.position, ObjEnemy.transform.forward.normalized * distance, Color.black);

        RaycastHit hit;
        //掃描線是否Hit到的是目標是的話返回True不是的話返回False
        if (Physics.Raycast(ObjEnemy.transform.position, eulerAnger * ObjEnemy.transform.forward.normalized * distance, out hit, distance,1<<LayerMask.NameToLayer("Player")) 
            || Physics.Raycast(ObjEnemy.transform.position, ObjEnemy.transform.forward.normalized * distance, out hit, distance,1<<LayerMask.NameToLayer("Player")) )
        {
            return true;
        }
        return false;

    }

}
