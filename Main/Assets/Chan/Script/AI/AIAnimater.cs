using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimater : MonoBehaviour
{
    public Animator ani;
    

    //簡易的動作切換
    public void EnemyAnimater(EnemyAni ANIMATER)
    {
        switch (ANIMATER)
        {
            case EnemyAni.IDLE:
                ani.Play("Idle");
                break;
            case EnemyAni.WALK:
                ani.Play("Walk");

                break;
            case EnemyAni.RUN:
                ani.Play("Run");

                break;
           
        }


    }

    //簡易的攻擊動作切換
    public void EnemyAttack(EnemyAni Attack)
    {
        switch (Attack)
        {
            case EnemyAni.ATTACK1:
                ani.Play("Attack1");

                break;
            case EnemyAni.ATTACK2:
                ani.Play("Attack2");

                break;

            case EnemyAni.ATTACK3:
                ani.Play("Attack3");

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
        ATTACK3
    }
}

