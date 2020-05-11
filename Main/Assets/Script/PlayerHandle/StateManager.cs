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
        PlayerUI.UIManager.instance.ShowPlayerHp();
    }
    private void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR");
        isSlash = am.ac.CheckState("slash");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        //isDefense = am.ac.CheckState("defenseR", "defense");
        isCounterBack = am.ac.CheckState("counterBack");
        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defenseR", "defense") && am.ac.pi.defense;
        isImmortal = isSlash || isRoll || isJab;
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
        if (gameObject.tag == "ServerSYNC")
        {
            playerHP.SetMaxHp(LoginManager.instance.client.tranmitter.mMessage.friend[transform.GetSiblingIndex()].maxHp);
            ATK = LoginManager.instance.client.tranmitter.mMessage.friend[transform.GetSiblingIndex()].atkDamage;
            playerHP.SetCurrentHP(LoginManager.instance.client.tranmitter.mMessage.friend[transform.GetSiblingIndex()].hp);
        }
        else if (gameObject.tag == "Player")
        {
            if (LoginManager.instance != null)
            {
                playerHP.SetMaxHp(LoginManager.instance.client.tranmitter.mMessage.myMaxHp);
                ATK = LoginManager.instance.client.tranmitter.mMessage.myAtkDamage;
                playerHP.SetCurrentHP(LoginManager.instance.client.tranmitter.mMessage.myHp);
            }
            else
            {
                playerHP.SetHP(80f, 80f);
                ATK = 15f;
            }

        }
        else if (gameObject.tag == "Npc")
        {
            if (LoginManager.instance != null)
            {
                playerHP.SetMaxHp(LoginManager.instance.client.tranmitter.mMessage.myEnemy[transform.GetSiblingIndex()].maxHp);
                ATK = LoginManager.instance.client.tranmitter.mMessage.myEnemy[transform.GetSiblingIndex()].atkDamage;
                playerHP.SetCurrentHP(LoginManager.instance.client.tranmitter.mMessage.myEnemy[transform.GetSiblingIndex()].hp);
            }
            else
            {
                playerHP.SetHP(80f, 80f);
                ATK = 15f;
            }
        }
        else
        {
            playerHP.SetHP(80f, 80f);
            ATK = 15f;
        }
    }
}    
