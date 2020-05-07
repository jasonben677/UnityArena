using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimater : MonoBehaviour
{
    public Animator ani;
    

    //簡易的動作切換
    public void EnemyAnimater(EnemyAni ANIMATER,AIData data)
    {
        switch (ANIMATER)
        {
            case EnemyAni.IDLE:
                ani.SetFloat("IdleRun",0);
                data.m_fSpeed = 0;
                data.m_fThinkTime = Random.Range(0.2f, 0.5f);
                break;
            case EnemyAni.WALK:
                ani.Play("Walk");

                break;
            case EnemyAni.RUN:
                ani.SetFloat("IdleRun",1);
                data.m_fSpeed += data.m_fMinSpeed;
                break;
            case EnemyAni.MOVINGLEFT:
                ani.Play("Movingleft");

                break;

            case EnemyAni.MOVINGRIGHT:
                ani.Play("Moveingright");

                break;

            case EnemyAni.WALKINGBACK:
                ani.Play("Walkingback");

                break;

            case EnemyAni.HIT:
                ani.Play("Hit");

                break;
            case EnemyAni.DIE:
                ani.Play("Die");
                break;

        }


    }

    //簡易的攻擊動作切換
    public void EnemyAttack(AIData data,EnemyAni Attack)
    {

        switch (Attack)
        {
            case EnemyAni.ATTACK1:
                ani.Play("Attack1");
                data.m_fSpeed = 0;

                break;
            case EnemyAni.ATTACK2:
                ani.Play("Attack2");

                break;

            case EnemyAni.ATTACK3:
                ani.Play("Attack3");

                break;
            case EnemyAni.SKILL1:
                ani.Play("Skill1");

                break;

            case EnemyAni.SKILL2:
                ani.Play("Skill2");

                break;

        }

    }


    
    public enum EnemyAni
    {
        NONE = 0,
        IDLE,
        WALK,
        RUN,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        SKILL1,
        SKILL2,
        WALKINGBACK,
        HIT,
        MOVINGLEFT,
        MOVINGRIGHT,
        DIE

    }
}

