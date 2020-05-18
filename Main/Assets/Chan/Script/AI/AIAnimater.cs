using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimater : MonoBehaviour
{
    public Animator ani;
    public GameObject EnemyFX;


    //簡易的動作切換
    public void EnemyAnimater(AIData data,EnemyAni ANIMATER)
    {
        switch (ANIMATER)
        {
            case EnemyAni.IDLE:
                ani.SetFloat("right",0);
                ani.SetFloat("forward", 0);
                data.m_fMaxSpeed = 0.0f;
                data.m_fSpeed = 0.0f;
               // data.m_fThinkTime = Random.Range(0.2f, 0.5f);
                break;
            case EnemyAni.WALK:
                ani.SetFloat("right", 0);
                ani.SetFloat("forward", 1);
                data.m_fMaxSpeed = 0.02f;

                break;
            case EnemyAni.RUN:
                ani.Play("ground");
                ani.SetFloat("right", 0);
                ani.SetFloat("forward", 2);
                if (data.m_fMaxSpeed <= 0.15)
                {
                    data.m_fMaxSpeed += Time.deltaTime;
                }
                else 
                {
                    data.m_fMaxSpeed = 0.15f; 
                }

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
                ani.SetFloat("forward", -1);
                data.m_fMaxSpeed = -0.05f;
                data.m_fMinSpeed = -0.05f;
                data.m_fSpeed = data.m_fMaxSpeed;
                break;

            case EnemyAni.HIT:
                ani.Play("hit");

                break;
            case EnemyAni.DIE:
                ani.Play("die");
                break;

            case EnemyAni.ATTACK:
                int i ;

                if (data.m_ObjEnemy.tag == "StrongNpc")
                {
                    i = Random.Range(1, 4);
                }
                else
                {
                    i = 1;
                }
                data.m_fMaxSpeed = 0.0f;
                data.m_fSpeed = 0.0f;
                EnemyAttack(data,i);
                break;

            case EnemyAni.ANGER:
                ani.Play("angry");
                data.bAnger = true;
                data.m_fMaxSpeed = 0.0f;
                data.m_fSpeed = 0.0f;
                break;
        }


    }

    //簡易的攻擊動作切換
    private void EnemyAttack(AIData data, int i)
    {
        float a = Random.Range(0.8f, 2f);

        switch (i)
        {
            case 1:
                ani.SetFloat("AttackTime",a);
                ani.Play("attack1hA");
                data.m_bAttack = true;
                break;
            case 2:
                ani.SetFloat("AttackTime", a);
                ani.Play("attack1hB");
                data.m_bAttack = true;
                break;
            case 3:
                ani.SetFloat("AttackTime", a);
                ani.Play("attack1hC");
                data.m_bAttack = true;
                break;
        }

    }


   


    public enum EnemyAni
    {
        NONE = 0,
        IDLE,
        WALK,
        RUN,
        ATTACK,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        SKILL1,
        SKILL2,
        WALKINGBACK,
        HIT,
        MOVINGLEFT,
        MOVINGRIGHT,
        DIE,
        ANGER

    }

     void OpenDieEnemyFX()
    {
    
            Instantiate(EnemyFX, this.gameObject.transform.position+this.transform.forward*-0.5f, this.gameObject.transform.rotation);
       


    }

     void CloseDieEnemyFX()
    {
        Destroy(EnemyFX);
    }
}

