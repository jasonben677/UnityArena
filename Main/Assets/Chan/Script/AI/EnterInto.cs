using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追擊判斷
public class EnterInto
{



   static public void aaaaa(AIData data,AIManage AImag,AIAnimater Ani)
    {
        if (AImag.m_bChase)
        {
            //判斷時間
            if (data.m_fThinkTime <= 0)
            {
                //轉方向
                AIBehaviour.Playerdirection(data);
                //追擊
                AIBehaviour.Move(data);
                //播放跑步動畫
                Ani.EnemyAnimater(AIAnimater.EnemyAni.RUN, data);

            }
            else
            {
                //判斷時間小於等於0
                data.m_fThinkTime -= Time.deltaTime;
            }
            Debug.Log(data.m_fSpeed);
        }
        else
        {

            if (EnterInto.AttackMode(data, AImag))
            {
                //if (AImag.m_iAttackRandom == 1)
                //{
                    Ani.EnemyAttack(data,AIAnimater.EnemyAni.ATTACK1, AImag);

                //}
                //else if (AImag.m_iAttackRandom == 2)
                //{
                //    Ani.EnemyAttack(AIAnimater.EnemyAni.ATTACK2, AImag);
                //}
                //else if (AImag.m_iAttackRandom == 3)
                //{
                //    Ani.EnemyAttack(AIAnimater.EnemyAni.ATTACK3, AImag);
                //}

            }
            else
            {

                Ani.EnemyAnimater(AIAnimater.EnemyAni.IDLE, data);
            }

        }

    }











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
                    data.m_fSpeed -= Time.deltaTime;

                    manage.m_bChase = false;
                    //玩家在範圍移動，怪物持續鎖定玩家
                    Quaternion targetRotation = Quaternion.LookRotation(data.ArrTarget[data.m_fID].transform.position - data.m_ObjEnemy.transform.position, Vector3.up);
                    data.m_ObjEnemy.transform.rotation = Quaternion.Slerp(data.m_ObjEnemy.transform.rotation, targetRotation, 5f);
                }
                else
                {

                    //目標脫離攻擊範圍 進入追擊 速度遞增恢復 追擊為True
                    data.m_fSpeed += Time.deltaTime;
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


   static public bool AttackMode(AIData data,AIManage aiman)
    {
        //抓取怪物位置
        Transform ObjEnemy = data.m_ObjEnemy.transform;
        Vector3 cpos = data.m_ObjEnemy.transform.position;
       
        
        
        //抓取怪物與目標的距離並轉換成長度
        Vector3 vec = data.m_vTarget - cpos;
        float m_fVec = vec.magnitude;


        Transform m_tEnemy = data.m_ObjEnemy.transform;




        //怪物前進

        //兩者間距離小於攻擊距離
        if (m_fVec <= aiman.m_fAttDis)
        {
            //確認攻擊
            return true;
        }
        else 
        {

            return false;
        }


        //攻擊結束後後退


    }
}
