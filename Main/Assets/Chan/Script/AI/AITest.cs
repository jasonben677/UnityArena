using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : PlayerInput
{
    public AIAnimater ani;
    [Header("------AITest-------")]
    private StateManager sm;
    private ActorManager am;
    private HealthPoint hp;
    [Header("------ClearTime------")]

    public float ClearTime;
    public float EmergeTime;
    [Header("------AIData------")]
    public AIData data;

    float NextHp;
    private void Awake()
    {
        //ani = GetComponent<AIAnimater>();
        sm = GetComponent<StateManager>();
        am = GetComponent<ActorManager>();
        //賦予所有怪物Layer為Enemy
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        //獲取所有Tag為Player的目標
        data.ArrTarget = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start()
    {
        //抓取HP腳本
        hp = gameObject.GetComponent<HealthPoint>();
    }




    void Update()
    {
        if (hp == null)
        {
            Start();
            //初始化數值
            Initialization();
        }

        data.fHP = hp.HP;
        CheackScope.LockTarget(data);

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            hp.HP -= 10;
        }
        else
        {
            if (data.fHP > 0)
            {
                //目標是否在範圍內
                if (EnterInto.EnterRange(data) == true)
                {
                    //前方是否有障礙物
                    if (SteeringBehaviour.CollisionAvoid(data) == false)
                    {
                        SteeringBehaviour.Seek(data);
                    }

                    //追擊判定
                    SteeringBehaviour.Move(data);
                    ani.EnemyAnimater(AIAnimater.EnemyAni.RUN, data);


                }
                else
                {
                    //暫時先放Idle,之後安插巡邏動畫
                    ani.EnemyAnimater(AIAnimater.EnemyAni.IDLE,data);
                }
            }
            else if (data.fHP <= 0)
            {
                if (ClearTime <= 0)
                {
                    ClearEnemy();
                }else
                {
                    ClearTime -= Time.deltaTime;
                }
                //撥放死亡動畫
                Debug.Log("is die");
            }

        }


        //Debug.Log(hp.MaxHP);


















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
            Gizmos.DrawWireSphere(this.transform.position, data.m_fAttDis);
            Vector3 vLeftStart = this.transform.position - this.transform.right * data.m_fRadius;
            Vector3 vLeftEnd = vLeftStart + this.transform.forward * data.m_fProbeLenght;
            Gizmos.DrawLine(vLeftStart, vLeftEnd);
            Vector3 vRightStart = this.transform.position + this.transform.right * data.m_fRadius;
            Vector3 vRightEnd = vRightStart + this.transform.forward * data.m_fProbeLenght;
            Gizmos.DrawLine(vRightStart, vRightEnd);
            Gizmos.DrawLine(vRightEnd, vLeftEnd);
        }



    }
    public void ClearEnemy()
    {
        Destroy(data.m_ObjEnemy);
    }

    private void Initialization()
    {



        data.fHP = hp.MaxHP;
        NextHp = data.fHP;
        data.m_fMaxSpeed = 0.15f;
        data.m_fMinSpeed = 0.02f;
        data.m_fMaxRot = 0.2f;
        data.m_fRadius = 1;
        data.m_fProbeLenght = 1;
        data.m_fPursuitRange = 20f;
        data.m_fAngle = 180;
        data.m_fThinkTime = Random.Range(0.2f, 0.5f);
        data.m_iAttackRandom = Random.Range(1, 3);
        data.m_fAttDis = 4f;


    }

}
