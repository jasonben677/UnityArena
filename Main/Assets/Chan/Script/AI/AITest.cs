using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    public Animator ani;

    public AIData data;

    void Start()
    {

        //賦予所有怪物Layer為Enemy
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        //獲取所有Tag為Player的目標
        data.ArrTarget = GameObject.FindGameObjectsWithTag("Player");

        ani = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        CheackScope.LockTarget(data);
        EnterInto.EnterRange(data);




        if (data.m_bChase)
        {
            
            AIBehaviour.Playerdirection(data);
            AIBehaviour.Move(data);
            

            EnemyAnimater(EnemyAni.RUN);

        }
        else
        {
            EnemyAnimater(EnemyAni.IDLE);
            Quaternion targetRotation = Quaternion.LookRotation(data.ArrTarget[data.m_fID].transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f);
        }
    }
    private void OnDrawGizmos()
    {
        if (data != null)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + transform.forward);

            Vector3 vLastTemp = Quaternion.Euler(0.0f, 30f, 0.0f) * -transform.right;


            //最近的範圍
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, data.m_fPursuitRange * 0.3f);
            //最遠的範圍
            Gizmos.color = Color.white;
            CheackScope.LookRange(data, 45, 135f, 1f);
            //中間的範圍
            Gizmos.color = Color.yellow;
            CheackScope.LookRange(data, -10, 190f, 0.8f);



        }



    }

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
