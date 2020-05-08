using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision 
{
    public void LookingPatrolPoint(AIData data)
    {
        //抓取怪物
        GameObject Enemy = data.m_ObjEnemy;
        Vector3 vEnemyPos = Enemy.transform.position;
        //抓取巡邏點
        GameObject Target = data.Patrolpoint;
        Vector3 vPos = Target.transform.position;





    }





    public void BattleNumber(AIData data)
    {
        //獲取怪物資料
        GameObject Enemy = data.m_ObjEnemy;
        Vector3 vEnemy = Enemy.transform.position;
        //獲取最近玩家資訊
       Vector3 vTarget= data.ArrTarget[data.m_fID].transform.position;



    }
}
