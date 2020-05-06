using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    AIAnimater Ani;
    public float ClearTime;
    public float EmergeTime;
    [Header("------AIData------")]
    public AIData data;
    [Header("------AIBoolAni------")]
    public AIManage AImag;
    public HealthPoint m_HP;

    public float NextHP;
    void Start()
    {

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
        data.m_bdie = true;
    }

    void Update()
    {

        CheackScope.LockTarget(data);


      
            SteeringBehaviour.Move(data);



        //按下F1 HP-1
        if (Input.GetKeyDown(KeyCode.F1)) m_HP.HP -= 10;
        //HP不足1的時候判斷死亡
        if (m_HP.HP <= 0)
        {
            //播放死亡動畫
            Ani.EnemyAnimater(AIAnimater.EnemyAni.DIE, data);
            if (data.m_bdie == true)
            {
                if (ClearTime <= 0)
                {
                    data.m_bdie = false;
                    EnemyDie();

                }
                else
                {
                    ClearTime -= Time.deltaTime;
                }
            }

        }
        else
        {
            EnterInto.EnterRange(data, AImag);

            if (SteeringBehaviour.CollisionAvoid(data) == false)
            {
                EnterInto.aaaaa(data, AImag, Ani);
            }
            else
            {
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
                }
            }
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, AImag.m_fAttDis);
            Vector3 vLeftStart = this.transform.position - this.transform.right * data.m_fRadius;
            Vector3 vLeftEnd = vLeftStart + this.transform.forward * data.m_fProbeLenght;
            Gizmos.DrawLine(vLeftStart, vLeftEnd);
            Vector3 vRightStart = this.transform.position + this.transform.right * data.m_fRadius;
            Vector3 vRightEnd = vRightStart + this.transform.forward * data.m_fProbeLenght;
            Gizmos.DrawLine(vRightStart, vRightEnd);
            Gizmos.DrawLine(vRightEnd, vLeftEnd);
        }



    }
    public void EnemyDie()
    {
        Destroy(data.m_ObjEnemy);
    }
}
