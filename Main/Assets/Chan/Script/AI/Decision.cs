using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : ActorController
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
