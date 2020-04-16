using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Data 
{   
    public float m_fRadius;
    public float m_fProbeLength;
    public float m_fSpeed;
    public float m_fMaxSpeed;
    public float m_fMinSpeed;
    public float m_fRot;
    public float m_fArriveDistance;

    public Transform m_Go;

    [HideInInspector]
    public Vector3 m_vTarget;
    [HideInInspector]
    public float m_fTargetDistance;
    [HideInInspector]
    public Vector3 m_fCurrentVector;
    [HideInInspector]
    public float m_fTempTurnForce;
    [HideInInspector]
    public float m_fMoveForce;

    [HideInInspector]
    public bool bMove;



}
