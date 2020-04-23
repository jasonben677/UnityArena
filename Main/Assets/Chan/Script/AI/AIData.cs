using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIData
{
   
    public GameObject m_gMoster;

    public float m_fSpeed;

    public float m_fMaxSpeed;

    public float m_fMinSpeed;

    public float m_fRadius;

    public float m_fProbeLenght;

    public float m_fPursuitRange;


    [HideInInspector]
    public Vector3 m_vTarget;
    [HideInInspector]
    public Vector3 m_vCurrentVector;

    public float m_fTempTurnForce;
}
