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

    [Header("2nd order state flag")]
    public bool isImmortal; //無敵狀態
    
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
