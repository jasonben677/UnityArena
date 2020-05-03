using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追擊判斷
public class EnterInto
{
    /// <summary>
    ///追擊範圍的判斷並且確認是否追擊
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    static public bool EnterRange(AIData data,AIManage manage)
    {
        //抓取怪物位置
        GameObject ObjEnemy = data.m_ObjEnemy;
        Vector3 cpos = ObjEnemy.transform.position;

        //抓取怪物與目標的距離並轉換成長度
        Vector3 vec = data.m_vTarget - cpos;
        float m_fVec = vec.magnitude;

        //data資料不為空的時候
        if (data != null)
        {
            //目標是否在怪物背後，怪物判斷玩家最近的範圍
            if (m_fVec < (data.m_fPursuitRange * 0.3f))
            {
                //怪物進入攻擊範圍
                if (m_fVec < data.AttRange)
                {
                    //速度要為0最好改成遞減 追擊為false
                    manage.m_bChase = false;
                    //玩家在範圍移動，怪物持續鎖定玩家
                    Quaternion targetRotation = Quaternion.LookRotation(data.ArrTarget[data.m_fID].transform.position - data.m_ObjEnemy.transform.position, Vector3.up);
                    data.m_ObjEnemy.transform.rotation = Quaternion.Slerp(data.m_ObjEnemy.transform.rotation, targetRotation, 5f);
                }
                else
                {

                    //目標脫離攻擊範圍 進入追擊 速度遞增恢復 追擊為True
                    manage.m_bChase = true;
                }
            }
            //目標不在最怪物背後
            else if (m_fVec > (data.m_fPursuitRange * 0.3f))
            {
                //怪物兩側的偵測範圍
                if (CheackScope.LookScope(data, data.m_fAngle / 2, 1))
                {

                    //追擊為true
                    manage.m_bChase = true;
                }
                else
                {
                    //怪物正前方最遠距裡的範圍
                    if (CheackScope.LookScope(data, data.m_fAngle + 20f, 0.8f))
                    {

                        //追擊為true
                        manage.m_bChase = true;
                    }
                    else
                    {
                        //追擊為false
                        manage.m_bChase = false;
                    }
                }
            }

        }
        //返回追擊數據
        return manage.m_bChase;

    }


    public void AttackMode(AIData data,AIManage aiman)
    {
        //抓取怪物位置
        Transform ObjEnemy = data.m_ObjEnemy.transform;
        Vector3 cpos = data.m_ObjEnemy.transform.position;
        Vector3 vR = ObjEnemy.right;
        Vector3 vF = ObjEnemy.forward;
        
        
        //抓取怪物與目標的距離並轉換成長度
        Vector3 vec = data.m_vTarget - cpos;
        float m_fVec = vec.magnitude;


        Transform m_tEnemy = data.m_ObjEnemy.transform;


        m_tEnemy.forward = data.m_vCurrentVector + vR * data.m_fTempTurnForce;
        m_tEnemy.forward.Normalize();
        m_tEnemy.forward = m_tEnemy.forward;

        //怪物前進

        //兩者間距離小於攻擊距離


        //確認攻擊

        //攻擊結束後後退


    }
}
