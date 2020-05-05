using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//AI所有的計算資料
public class SteeringBehaviour
{

    /// <summary>
    ///怪物的移動於轉向判斷
    /// </summary>
    /// <param name="data"></param>
    static public void Move(AIData data)
    {

        if (data.m_bMove == false) 
        {
            return;
        }


        Transform m_tEnemy = data.m_ObjEnemy.transform;
        Vector3 cPos = data.m_ObjEnemy.transform.position;
        Vector3 vR = m_tEnemy.right;
        Vector3 vOriF = m_tEnemy.forward;
        Vector3 vF = data.m_vCurrentVector;
        if (data.m_fTempTurnForce > data.m_fMaxRot)
        {
            data.m_fTempTurnForce = data.m_fMaxRot;
        }
        else if (data.m_fTempTurnForce < -data.m_fMaxRot)
        {
            data.m_fTempTurnForce = -data.m_fMaxRot;

        }

        vF = vF + vR * data.m_fTempTurnForce;
        vF.Normalize();
        m_tEnemy.forward = vF;


        data.m_fSpeed = data.m_fSpeed+data.m_fMoveforce * Time.deltaTime;
        if (data.m_fSpeed < data.m_fMinSpeed)
        {
            data.m_fSpeed = data.m_fMinSpeed;
        }
        else if (data.m_fSpeed > data.m_fMaxSpeed)
        {
            data.m_fSpeed = data.m_fMaxSpeed;
        }

        if (data.m_bCol==false)
        {
            if (SteeringBehaviour.CheckCollision(data))
            {
                m_tEnemy.forward = vOriF;
            }
        }
        else
        {
            if (data.m_fSpeed < 0.02f)
            {
                if (data.m_fTempTurnForce > 0)
                {
                    m_tEnemy.forward = vR;
                }
                else
                {
                    m_tEnemy.forward = -vR;
                }
            }
        }



        cPos = cPos + data.m_fSpeed * m_tEnemy.forward;
        m_tEnemy.position = cPos;
    }

    //檢查碰撞
    static public bool CheckCollision(AIData data)
    {
        List<Obstacle> m_AvoidTargets = Main.m_Instance.GetObstacles();

        if (m_AvoidTargets == null)
        {
            return false;
        }
        Transform ct = data.m_ObjEnemy.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        Vector3 vec;

        float fDis = 0.0f;
        float fDot = 0.0f;
        int iCount = m_AvoidTargets.Count;
        for (int i = 0; i < iCount; i++)
        {
            vec = m_AvoidTargets[i].transform.position - cPos;
            vec.y = 0;
            fDis = vec.magnitude;
            if (fDis > data.m_fProbeLenght + m_AvoidTargets[i].m_fRadius)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }
            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }
            m_AvoidTargets[i].m_eState = Obstacle.eState.INSIDE_TEST;
            float fProJDist = fDis * fDot;
            float fDotDist = Mathf.Sqrt(fDis * fDis - fProJDist * fProJDist);
            if (fDotDist > m_AvoidTargets[i].m_fRadius + data.m_fRadius)
            {
                continue;
            }
            return true;
        }

        return false;
    }


    //碰撞迴避

    static public bool CollisionAvoid(AIData data)
    {
        List<Obstacle> m_AvoidTarget = Main.m_Instance.GetObstacles();
        Transform ct = data.m_ObjEnemy.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        data.m_vCurrentVector = cForward;
        Vector3 vec;
        float fFinalDotDist;
        float fFinalProjDist;
        Vector3 vFinalVec = Vector3.forward;
        Obstacle oFinal = null;
        float fDist = 0.0f;
        float fDot = 0.0f;
        float fFinalDot = 0.0f;
        int iCount = m_AvoidTarget.Count;

        float fMinDist = 10000.0f;
        for (int i = 0; i < iCount; i++)
        {
            vec = m_AvoidTarget[i].transform.position - cPos;
            vec.y = 0.0f;
            fDist = vec.magnitude;
            if (fDist > data.m_fProbeLenght + m_AvoidTarget[i].m_fRadius)
            {
                m_AvoidTarget[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }
            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0)
            {
                m_AvoidTarget[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }
            else if (fDot > 1.0f)
            {
                fDot = 1.0f;
            }
            m_AvoidTarget[i].m_eState = Obstacle.eState.INSIDE_TEST;
            float fProjDist = fDist * fDist;
            float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
            if (fDotDist > m_AvoidTarget[i].m_fRadius + data.m_fRadius)
            {
                continue;
            }

            if (fDist < fMinDist)
            {
                fMinDist = fDist;
                fFinalDotDist = fDotDist;
                fFinalProjDist = fProjDist;
                vFinalVec = vec;
                oFinal = m_AvoidTarget[i];
                fFinalDot = fDot;
            }

        }
        if (oFinal != null)
        {
            Vector3 vCross = Vector3.Cross(cForward, vFinalVec);
            float fTurnMag = Mathf.Sqrt(1.0f - fFinalDot * fFinalDot);
            if (vCross.y > 0.0f)
            {
                fTurnMag = -fTurnMag;
            }
            data.m_fTempTurnForce = fTurnMag;

            float fTotalLen = data.m_fProbeLenght + oFinal.m_fRadius;
            float fRatio = fMinDist / fTotalLen;
            if (fRatio > 1.0f)
            {
                fRatio = 1.0f;
            }
            fRatio = 1.0f - fRatio;
            data.m_fMoveforce = -fRatio;
            oFinal.m_eState = Obstacle.eState.COL_TEST;
            data.m_bCol = true;
            data.m_bMove = true;
            return true;



        }
        data.m_bCol = false;
        return false;
    }











    //static public void Away(AIData data, AIManage AImag)
    //{
    //    AImag.m_iAttackRandom = Random.Range(1, 3);

    //}


    //static public void TargetRotate(AIData data)
    //{
    //    GameObject Enemy = data.m_ObjEnemy;
    //    //持續怪物繞著目標旋轉
    //    Enemy.transform.RotateAround(data.ArrTarget[data.m_fID].transform.position, Vector3.up, data.m_fSpeed * Time.deltaTime);


    //}
    /// <summary>
    ///抓取目標位置並獲取轉向力
    /// </summary>
    /// <param name="data"></param>
    static public bool Seek(AIData data)
    {

        Vector3 cPos = data.m_ObjEnemy.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0;
        float fDis = vec.magnitude;
        data.m_fTargetDistance = fDis;
        if (fDis < data.m_fSpeed + 0.001f)
        {
            Vector3 vFinal = data.m_vTarget;
            vFinal.y = cPos.y;
            data.m_fTargetDistance = 0.0f;
            data.ArrTarget[data.m_fID].transform.position = vFinal;
            data.m_fMoveforce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_fSpeed = 0.0f;
            data.m_bMove = false;
            return false;
        }
        data.m_fMinSpeed = 0.2f;

        Vector3 vf = data.m_ObjEnemy.transform.forward;
        Vector3 vr = data.m_ObjEnemy.transform.right;
        data.m_vCurrentVector = vf;
        vec.Normalize();
        float fDotF = Vector3.Dot(vf, vec);



        if (fDotF > 0.96f)
        {
            fDotF = 1.0f;
            data.m_vCurrentVector = vec;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else
        {
            if (fDotF < -1.0f)
            {
                fDotF = -1.0f;
            }
            float fDotR = Vector3.Dot(vr, vec);

            if (fDotF < 0)
            {
                if (fDotR > 0.0f)
                {
                    fDotR = 1.0f;
                }
                else
                {
                    fDotR = -1.0f;
                }
            }

            data.m_fTempTurnForce = fDotR;
        }
        data.m_fMoveforce = fDotF * 100f;
        data.m_bMove = true;
        return true;
    }
    
}
