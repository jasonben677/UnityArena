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
                ani.SetFloat("right",0);
                ani.SetFloat("forward", 0);
                data.m_fSpeed = 0;
                data.m_fThinkTime = Random.Range(0.2f, 0.5f);
                break;
            case EnemyAni.WALK:
                ani.SetFloat("right", 0);
                ani.SetFloat("forward", 1);
                ani.Play("Walk");

                break;
            case EnemyAni.RUN:
                ani.SetFloat("right", 0);
                ani.SetFloat("forward", 2);
                data.m_fSpeed += data.m_fMinSpeed;
                break;
            case EnemyAni.MOVINGLEFT:
                ani.SetFloat("right", -1);
                ani.SetFloat("forward", 0);

                break;

            case EnemyAni.MOVINGRIGHT:
                ani.SetFloat("right", 1);
                ani.SetFloat("forward", 0);

                break;

            case EnemyAni.WALKINGBACK:
                ani.SetFloat("right", 0);
                ani.SetFloat("forward", 1);

                break;

            case EnemyAni.HIT:
                ani.Play("hit");

                break;
            case EnemyAni.DIE:
                ani.Play("die");
                break;

        }


    }

    //簡易的攻擊動作切換
    public void EnemyAttack(AIData data,EnemyAni Attack)
    {

        switch (Attack)
        {
            case EnemyAni.ATTACK1:
                ani.Play("attack1hA");
                data.m_fSpeed = 0;

                break;
            case EnemyAni.ATTACK2:
                ani.Play("attack1hB");

                break;

            case EnemyAni.ATTACK3:
                ani.Play("attack1hC");

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

