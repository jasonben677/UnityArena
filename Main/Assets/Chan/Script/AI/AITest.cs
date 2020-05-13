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

    #region 抓取資料
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
        data.OnTreeEnemys = GameObject.FindGameObjectsWithTag("Npc");
        WanderPoint = Decision.LookingPatrolPoint(data);
        ani = gameObject.GetComponentInChildren<AIAnimater>();

    }
    void Start()
    {
        //抓取HP腳本
        hp = gameObject.GetComponent<HealthPoint>();
        Initialization();
    }
    #endregion

    //-------------------------重點---------------------------很重要所以要說三次
    //                  Update改名NpcUpdate
    //                  Update改名NpcUpdate
    //                  Update改名NpcUpdate
    //public void Update()//<-------------開啟自測試用

    //個人測試時開啟
    //public void Update()
    //{
    //    NpcUpdate();
    //}






    public void NpcUpdate()
    {
        #region 初始化設定用
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

        #endregion
        
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
                       // AttackTest();

                    }
                    else
                    {
                        RunAttTime -= Time.deltaTime;
                        Quaternion targetRotation = Quaternion.LookRotation(data.ArrTarget[data.m_fID].transform.position - data.m_ObjEnemy.transform.position, Vector3.up);
                        data.m_ObjEnemy.transform.rotation = Quaternion.Slerp(data.m_ObjEnemy.transform.rotation, targetRotation, 5f);
                        ani.EnemyAnimater(data, AIAnimater.EnemyAni.ANGER);

                        IdleTime = Random.Range(0.2f, 0.5f);
                        data.m_fPursuitRange = data.m_fPursuitRange * 3;
                    }

                }
                else
                {
                    //巡邏判定
                    EnemyPatrol();
                    RunAttTime = Random.Range(0.6f, 1f);
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

    #region 畫範圍用
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
    #endregion

    #region 刪除怪物
    //刪除死亡怪物
    public void ClearEnemy()
    {
        data.m_ObjEnemy.SetActive(false);
    }
    #endregion

    #region 巡邏用
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
    #endregion

    // 攻擊判斷
    void AttackStatus()
    {
        if (EnterInto.Distance(data, data.m_vTarget, data.m_fAttDis) == true)
        {
            IdleTime = Random.Range(1f, 2.5f);

            // 攻擊時間判斷
            if (AttackTime >= 0)
            {

                //這裡可以做出怪物的警戒
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
                AttackTime = 0f;
            }
        }
    }

    void AttackTest()
    {
        //怪物進入警戒圈
        if (EnterInto.Distance(data, data.m_vTarget, data.m_fAlertDis) == true)
        {
            //警界時間=AttackTime 進入攻擊
            //if (AttackTime <= 0)
            //{
                //判斷是否在攻擊範圍內
                if (EnterInto.Distance(data, data.m_vTarget, data.m_fAttDis) == true)
                {

                    if (AttackTime <= 0)
                    {
                        //怪物攻擊判定內部判定要甚麼攻擊狀態
                        ani.EnemyAnimater(data, AIAnimater.EnemyAni.ATTACK);
                        //攻擊時間
                        AttackTime = Random.Range(2, 4);

                    }
                    else
                    { 
                        //這裡可以做出怪物的警戒
                        AttackTime -= Time.deltaTime;
                        //怪物IDLE
                        ani.EnemyAnimater(data, AIAnimater.EnemyAni.WALKINGBACK);
                        SteeringBehaviour.Seek(data, data.m_vTarget);
                        SteeringBehaviour.Move(data);

                }
                }
                else
                {
                    //往前移動到攻擊範圍
                    SteeringBehaviour.Seek(data, data.m_vTarget);
                    SteeringBehaviour.Move(data);
                    ani.EnemyAnimater(data, AIAnimater.EnemyAni.RUN);

                }
            //}
            //else
            //{

            //    //這裡可以新增鎖定玩家左右走動
            //    //如果沒有
            //    ani.EnemyAnimater(data, AIAnimater.EnemyAni.IDLE);
            //    AttackTime -= Time.deltaTime;
            //}

        }
        else
        {//否則繼續巡邏
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


    #region 初始化設定
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
        IdleTime = Random.Range(1f, 3f);
        AttackTime = 0;
        //hp.SetMaxHp(40);
        RunAttTime = Random.Range(0.6f, 1f);
        data.m_fAlertDis = 4f;
        data.CallRange = 30f;
        this.data.m_ObjEnemy.SetActive(data.OnTag);
    }
    #endregion
}
