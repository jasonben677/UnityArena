using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;    
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;

    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;
        bm = sensor.GetComponent<BattleManager>();
        if (bm == null)
        {
            bm = sensor.AddComponent<BattleManager>();
        }
        bm.am = this;

        wm = model.GetComponent<WeaponManager>();
        if (wm == null)
        {
            wm = model.GetComponent<WeaponManager>();
        } 
        wm.am = this;

        sm = gameObject.GetComponent<StateManager>();
        //if (sm = null)
        //{
        //    sm = gameObject.AddComponent<StateManager>();
        //}
        //sm.am = this;

        //Lom = gameObject.AddComponent<LoginManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //sm.Test();
        //LoginManager.instance?.SetMPlayerHP(transform, sm.playerHP.HP, wm.wcR.GetATK());

        if (LoginManager.instance != null && gameObject.tag == "Player")
        {
            sm.playerHP.SetCurrentHP(LoginManager.instance.GetMPlayerHP());
        }
    }

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid, bool counterValid)
    {
        if (sm.isCounterBackSuccess) //反擊成功
        {
            if (counterValid)
            {
                targetWc.wm.am.Stunned();
            }
        }
        else if (sm.isCounterBackFailure) //反擊失敗
        {
            if (attackValid)
            {
                HitOrDie(targetWc, true);
            }
        }
        else if (sm.isImmortal) //無敵狀態
        {
            //Do nothing
        }
        //else if (sm.isDefense) //防禦狀態
        //{
        //    Blocked();
        //}
        else
        {
            if (attackValid)
            {
                HitOrDie(targetWc, true);
            }
        }
    }

    public void HitOrDie(WeaponController targetWc ,bool doHitAnimation)
    {      
        if (ac.pi.isAI && targetWc.wm.am.transform.gameObject.layer == 10) //避免Npc打Npc的問題
        {
            return;
        }
        else
        {
            if (sm.playerHP.HP <= 0)
            {
                if (ac.CheckState("die"))
                {
                    Debug.Log("死亡");
                }
                else
                {
                    Die();
                    if (gameObject.tag == "Player")
                    {
                        LoginManager.instance.ScenceFadeOut();
                    }

                }
                //Already dead
            }
            else
            {
                if (gameObject.tag == "ServerSYNC")
                {
                    LoginManager.instance.GetHitUpdateHpAndAtk(transform.GetSiblingIndex(), (int)targetWc.GetATK());
                }
                else if (gameObject.tag == "Player")
                {
                    //LoginManager.instance.GetHitUpdateHpAndAtk(-1, (int)targetWc.GetATK());
                }
                else
                {
                    sm.playerHP.AddHP(-1 * targetWc.GetATK());
                }

                if (sm.playerHP.HP > 0)
                {
                    if (doHitAnimation)
                    {
                        Hit();
                    }
                    //do some VFX, like splatter blood...
                }
                else
                {
                    Debug.Log(transform.name + " IS DEAD");
                    Die();
                }

                PlayerUI.UIManager.instance.HitPlayer(gameObject);
            }
        }  
    }

    public void Stunned()
    {
        //Debug.Log("stunned");
        ac.IssueTrigger("stunned");
    }

    public void Blocked()
    {
        //Debug.Log("blocked");
        ac.IssueTrigger("blocked");
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        if(ac.pi.isAI == false)
        {
            ac.pi.inputEnable = false;
            if (ac.camcon.lockState == true)
            {
                ac.camcon.LockUnlock();                
            }
            ac.camcon.enabled = false;
        }
        
    }
}
