using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : DummyIUserInput
{
    AIAnimater Ani;
    [Header("------ClearTime------")]

    public float ClearTime;
    public float EmergeTime;
    [Header("------AIData------")]
    public AIData data;



    private void Awake()
    {
        //賦予所有怪物Layer為Enemy
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        //獲取所有Tag為Player的目標
        data.ArrTarget = GameObject.FindGameObjectsWithTag("Player");
        //抓取動作腳本
        Ani = GetComponent<AIAnimater>();

    }
    void Start()
    {
        data.m_fThinkTime = Random.Range(0.2f, 0.5f);
        data.m_iAttackRandom = Random.Range(1, 3);
       
    }

    void Update()
    {

        CheackScope.LockTarget(data);

        //if (EnterInto.EnterRange(data) == true)
        //{
        //    if (SteeringBehaviour.CollisionAvoid(data) == false)
        //    {
        //        SteeringBehaviour.Seek(data);
        //    }
           // SteeringBehaviour.Move(data);


        //}
        //else 
        //{
        //    SteeringBehaviour.Seek(data);

        //}
























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
    public void EnemyDie()
    {
        Destroy(data.m_ObjEnemy);
    }
}
