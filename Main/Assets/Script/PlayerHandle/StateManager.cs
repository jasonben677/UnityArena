using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public ActorManager am;    
    public HealthPoint playerHP;

    //public float HPMax = 100.0f;
    //public float HP = 15.0f;
    public float ATK = 10.0f;

    [Header("1st order state flags")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isSlash;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isStunned;
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
        playerHP = gameObject.AddComponent<HealthPoint>();

        _InitPlayHpAndAtk();

        //UI顯示
        //PlayerUI.UIManager.instance.UpdatePlayerUI();
    }
    private void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR") || am.ac.CheckStateTag("attackRX");
        isSlash = am.ac.CheckState("slash");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        isStunned = am.ac.CheckState("stunned");
        //isDefense = am.ac.CheckState("defenseR", "defense");
        isCounterBack = am.ac.CheckState("counterBack");
        isCounterBackSuccess = isCounterBack && isCounterBackEnable && am.ac.pi.defense;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defenseR", "defense") && am.ac.pi.defense;
        isImmortal = isSlash || isRoll || isJab || am.ac.CheckStateTag("attackRX");
    }

    //public void AddHP(float value)
    //{
    //    playerHP.HP += value;
    //    playerHP.HP = Mathf.Clamp(playerHP.HP, 0, playerHP.MaxHP);        
    //}    

    public void Test()
    {
        Debug.Log("sm test: HP is" + playerHP.HP);
    }


    /// <summary>
    /// 初始化所有hp和攻擊力
    /// </summary>
    private void _InitPlayHpAndAtk()
    {
        if (gameObject.tag == "Player")
        {
            PlayerInfo tempPlayer = NumericalManager.instance.GetMainPlayer();
            playerHP.SetHP(tempPlayer.fPlayerMaxHp, tempPlayer.fPlayerHp);
            ATK = NumericalManager.instance.GetMainPlayer().fAtk;

        }
        else if (gameObject.tag == "Npc")
        {
            PlayerInfo tempNpc = NumericalManager.instance.GetNpc(transform.GetSiblingIndex());
            playerHP.SetHP(tempNpc.fPlayerMaxHp, tempNpc.fPlayerHp);
            ATK = tempNpc.fAtk;
        }
        else if (gameObject.tag == "Boss")
        {
            PlayerInfo tempNpc = NumericalManager.instance.GetBoss();
            playerHP.SetHP(tempNpc.fPlayerMaxHp, tempNpc.fPlayerHp);
            ATK = tempNpc.fAtk;
        }
        else if (gameObject.tag == "StrongNpc")
        {
            PlayerInfo tempNpc = NumericalManager.instance.GetStrongNpc(transform.GetSiblingIndex());
            playerHP.SetHP(tempNpc.fPlayerMaxHp, tempNpc.fPlayerHp);
            ATK = tempNpc.fAtk;
        }
        else if(gameObject.tag == "Spider")
        {
            PlayerInfo tempNpc = NumericalManager.instance.GetSpider();
            playerHP.SetHP(tempNpc.fPlayerMaxHp, tempNpc.fPlayerHp);
            ATK = tempNpc.fAtk;
        }
        else
        {
            playerHP.SetHP(80f, 80f);
            ATK = 15f;
        }
    }
}    
