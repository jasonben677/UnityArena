using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public ActorManager am;

    public float HPMax = 15.0f;
    public float HP = 15.0f;

    [Header("1st order state flags")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack;    //related to state
    public bool isCounterBackEnable;  //related to animation event

    [Header("2nd order state flag")]
    public bool isAllowDefense;
    public bool isImmortal; //無敵狀態
    public bool isCounterBackSuccess;
    public bool isCounterBackFailure;
    
    private void Start()
    {
        am = gameObject.GetComponent<ActorManager>();
        //AddHP(0);
        HP = HPMax;
    }
    private void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        //isDefense = am.ac.CheckState("defenseR", "defense");
        isCounterBack = am.ac.CheckState("counterBack");
        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defenseR", "defense");
        isImmortal = isRoll || isJab;
    }

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);        
    }    

    public void Test()
    {
        Debug.Log("sm test: HP is" + HP);
    }
}    
