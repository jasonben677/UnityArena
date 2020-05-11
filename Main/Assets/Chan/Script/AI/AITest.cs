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
    public bool isPlayerDie;
    [Header("------ClearTime------")]

    public float ClearTime;
    public float IdleTime;
    public float AttackTime;
    public float RunAttTime;
    [Header("------AIData------")]
    public AIData data;

    public float NextHp;
    public GameObject WanderPoint;
    private void Awake()
    {
        //ani = GetComponent<AIAnimater>();
        sm = GetComponent<StateManager>();
        am = GetComponent<ActorManager>();
        //賦予所有怪物Layer為Enemy
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        //獲取所有Tag為Player的目標
        data.ArrTarget = GameObject.FindGameObjectsWithTag("Player");
        //抓取第一次移動點
        WanderPoint = Decision.LookingPatrolPoint(data);
        ani = gameObject.GetComponentInChildren<AIAnimater>();

    }
    void Start()
    {
        //抓取HP腳本
        hp = gameObject.GetComponent<HealthPoint>();
        Initialization();
    }


    //-------------------------重點---------------------------很重要所以要說三次
    //                  Update改名NpcUpdate
    //                  Update改名NpcUpdate
    //                  Update改名NpcUpdate

    public void Update()
    {
        //防止抓不到腳本
        if (hp == null)
        {
            Start();
            //初始化數值
        }

        data.fHP = hp.HP;

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            //hp.HP -= 10;
            hp.AddHP(-10);
            ani.EnemyAnimater(data, AIAnimater.EnemyAni.HIT);

        }
        CheackScope.LockTarget(data);
        isPlayerDie = data.ArrTarget[data.m_fID].GetComponent<StateManager>().isDie;


        //怪物HP>0
        if (data.fHP > 0)
        {
            //Player Die 了沒
            if (isPlayerDie == false)
            {
                //目標是否在範圍內
                if (EnterInto.EnterRange(data) == true)
                {
                    if (RunAttTime <= 0)
                    {
                        //攻擊距離是否成立
                        AttackStatus();

                    }
                    else 
                    {
                        RunAttTime -= Time.deltaTime;
                        ani.EnemyAnimater(data,AIAnimater.EnemyAni.IDLE);
                        IdleTime = Random.Range(0.5f, 1f);

                    }

                }
                else
                {
                   //巡邏判定
                    EnemyPatrol();
                    RunAttTime = Random.Range(1f, 3f);
                }
            }
            else if (isPlayerDie == true)//以下為巡邏
            {
                EnemyPatrol();
            }

        }
        else if (data.fHP <= 0)
        {
            if (ClearTime <= 0)
            {
                ClearEnemy();
            }
            else
            {
                ClearTime -= Time.deltaTime;
            }
            //撥放死亡動畫
            ani.EnemyAnimater(data, AIAnimater.EnemyAni.DIE);
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


    //初始化設定
    private void Initialization()
    {
        //數值得初始化
        data.fHP = hp.MaxHP;
        NextHp = data.fHP;
        data.m_fMaxSpeed = 0.15f;
        data.m_fMinSpeed = 0.02f;
        data.m_fMaxRot = 0.1f;
        data.m_fRadius = 1;
        data.m_fProbeLenght = 1;
        data.m_fPursuitRange = 20f;
        data.m_fAngle = 180;
      //  data.m_fThinkTime = Random.Range(0.2f, 0.5f);
        data.m_iAttackRandom = Random.Range(1, 3);
        data.m_fAttDis = 2f;
        ClearTime = 3f;
        data.AttRange = 4f;
        IdleTime = Random.Range(1f, 3f);
        AttackTime = Random.Range(1f, 3f);
        hp.SetMaxHp(40);
        RunAttTime = Random.Range(1f, 3f);
    }


    //巡邏
    void EnemyPatrol()
    {
        //WanderPoint的位子與怪物位子距離<=1時重新獲取下個WanderPoint
        if ((data.m_ObjEnemy.transform.position - WanderPoint.transform.position).magnitude <= 1)
        {
            WanderPoint = Decision.LookingPatrolPoint(data);
            IdleTime = Random.Range(1f, 3f);

        }
        else
        {
            if (IdleTime <= 0)
            {
                if (SteeringBehaviour.CollisionAvoid(data) == false)
                {
                    SteeringBehaviour.Seek(data, WanderPoint.transform.position);
                }
                ani.EnemyAnimater(data, AIAnimater.EnemyAni.WALK);
                SteeringBehaviour.Move(data);
            }
            else
            {
                IdleTime -= Time.deltaTime;
                ani.EnemyAnimater(data, AIAnimater.EnemyAni.IDLE);
            
            }
        }
    }
    void AttackStatus() 
    {
        if (EnterInto.AttackDistance(data, data.m_vTarget) == true)
        {
            IdleTime = Random.Range(0.5f, 1f);

            // 攻擊時間判斷
            if (AttackTime >= 0)
            {
                AttackTime -= Time.deltaTime;
                //怪物IDLE
                ani.EnemyAnimater(data, AIAnimater.EnemyAni.IDLE);
            }
            else
            {
                //怪物攻擊判定內部判定要甚麼攻擊狀態
                ani.EnemyAnimater(data, AIAnimater.EnemyAni.ATTACK);
                //攻擊時間
                AttackTime = Random.Range(2, 4);
            }
        }
        else
        {
            if (IdleTime <= 0)
            {
                //前方是否有障礙物
                if (SteeringBehaviour.CollisionAvoid(data) == false)
                {
                    SteeringBehaviour.Seek(data, data.ArrTarget[data.m_fID].transform.position);
                }
                //追擊判定
                SteeringBehaviour.Move(data);
                ani.EnemyAnimater(data, AIAnimater.EnemyAni.RUN);
            }
            else
            {
                IdleTime -= Time.deltaTime;
                ani.EnemyAnimater(data, AIAnimater.EnemyAni.IDLE);
            }
        }
    }

}
