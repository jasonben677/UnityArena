using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    AIAnimater Ani;
    [Header("------AIData------")]
    public AIData data;
    [Header("------AIBoolAni------")]
    public AIManage AImag;
    public HealthPoint m_HP;

    public NavMeshAgent agent;
    public float NextHP;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //賦予所有怪物Layer為Enemy
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        //獲取所有Tag為Player的目標
        data.ArrTarget = GameObject.FindGameObjectsWithTag("Player");
        //抓取動作腳本
        Ani = GetComponent<AIAnimater>();
        data.m_fThinkTime = Random.Range(0.2f, 0.5f);
        AImag.m_iAttackRandom = Random.Range(1, 3);
        m_HP.HP = m_HP.MaxHP;
        NextHP = m_HP.HP;
    }

    void Update()
    {

        CheackScope.LockTarget(data);
        EnterInto.EnterRange(data, AImag);

        //按下F1 HP-1
        if (Input.GetKeyDown(KeyCode.F1)) m_HP.HP -= 1;

        //現在HP小於上個HP
        if (m_HP.HP < NextHP)
        {
            //受傷動畫
            Ani.EnemyAnimater(AIAnimater.EnemyAni.HIT, data);
            //下個HP等於現在HP
            NextHP = m_HP.HP;
        }
        else
        {
            EnterInto.aaaaa(data, AImag, Ani);


        }


        #region
        /*
        if (m_AImag.m_bChase)
        {
            //發現停頓
            if (data.m_fThinkTime <= 0)
            {

                //轉向立
                AIBehaviour.Playerdirection(data);
                //追擊
                AIBehaviour.Move(data);
                //撥放跑步動畫
                Ani.EnemyAnimater(AIAnimater.EnemyAni.RUN);
            }
            else
            {
                //發呆時間
                data.m_fThinkTime -= Time.deltaTime;

            }

        }
        else
        {
            //發呆時間刷新
            data.m_fThinkTime = Random.Range(0.2f, 0.5f);
            //攻擊選擇判定
           // EnterInto.AttackMode(data, m_AImag);

            //攻擊判斷
            if (m_AImag.m_bAttack)
            {
                //攻擊選擇

                if (m_AImag.m_iAttackRandom == 1)
                {

                    Ani.EnemyAttack(AIAnimater.EnemyAni.ATTACK1);

                }
                else if (m_AImag.m_iAttackRandom == 2)
                {
                    Ani.EnemyAttack(AIAnimater.EnemyAni.ATTACK2);
                }
                else if (m_AImag.m_iAttackRandom == 3)
                {
                    Ani.EnemyAttack(AIAnimater.EnemyAni.ATTACK3);
                }
            }
            else
            {
                m_AImag.m_iAttackRandom = Random.Range(1, 3);
                EnterInto.AttackMode(data, m_AImag);

            }
            //甚麼都沒看到進入Idle
            Ani.EnemyAnimater(AIAnimater.EnemyAni.IDLE);

        }
 */
        #endregion
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, AImag.m_fAttDis);


        }



    }



}
