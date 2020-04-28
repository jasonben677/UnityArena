﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIData
{
    //怪物
    public GameObject m_ObjEnemy;

    //所有目標
    public GameObject[] ArrTarget;

    //速度
    public float m_fSpeed;

    //最大速度
    public float m_fMaxSpeed;

    //最小速度
    public float m_fMinSpeed;

    //半徑
    public float m_fRadius;


    //探針長度
    public float m_fProbeLenght;

    //追擊範圍
    public float m_fPursuitRange;

    //追擊確認
    public bool m_bChase;


    //目標的位置
    //[HideInInspector]
    public Vector3 m_ArrayVTarget;


    //當前向量
    [HideInInspector]
    public Vector3 m_vCurrentVector;

    //每秒旋轉
    [HideInInspector]
    public float m_fRotatePerSecond;

    //暫時的旋轉力度
    [HideInInspector]
    public float m_fTempTurnForce;
}
