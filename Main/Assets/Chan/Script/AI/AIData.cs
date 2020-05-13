using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIData
{
    //怪物HP量
    public float fHP;

    //怪物
    public GameObject m_ObjEnemy;

    //怪物的鄰居(怪物附近的怪物)
    public GameObject[] NeighborEnemys;

    //巡邏點
    public GameObject[] ArrWanderPoint;

    //所有目標
    public GameObject[] ArrTarget;

    //影藏的怪物
    public GameObject[] OnTreeEnemys;

    //速度
    public float m_fSpeed;

    //最大速度
    public float m_fMaxSpeed;

    //最小速度
    public float m_fMinSpeed;

    //轉向力
    public float m_fRot;
    //最大轉向力
    public float m_fMaxRot;

    //怪物呼叫的範圍
    public float CallRange;

    //半徑
    public float m_fRadius;

    //警戒範圍
    public float m_fAlertDis;
     
    //探針長度
    public float m_fProbeLenght;

    //追擊範圍
    public float m_fPursuitRange;

    //可視角度
    public float m_fAngle;

    //思考時間
    //public float m_fThinkTime;

    //是否死亡
    public bool m_bdie;


    //攻擊動作決定
    public int m_iAttackRandom;

    //攻擊距離
    public float m_fAttDis;

    //追擊確認
     public bool m_bChase;

    //攻擊判斷

    public bool m_bAttack;



    //鎖定目標的ID
    [HideInInspector]
    public int m_fID;
    //目標的位置
    [HideInInspector]
    public Vector3 m_vTarget;
    //當前向量
    [HideInInspector]
    public Vector3 m_vCurrentVector;
    //每秒旋轉
    [HideInInspector]
    public float m_fRotatePerSecond;
    //暫時的旋轉力度
    [HideInInspector]
    public float m_fTempTurnForce;
    [HideInInspector]
    public float m_fMoveforce;
    [HideInInspector]
    public bool m_bMove;
    [HideInInspector]
    public bool m_bCol;

    [HideInInspector]
    public float m_fTargetDistance;
}
