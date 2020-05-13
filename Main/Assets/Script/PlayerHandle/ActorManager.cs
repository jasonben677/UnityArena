using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;    
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;

    [Header("==== Prefabs ====")]
    [SerializeField]
    private GameObject bloodParticle;
    [SerializeField]
    private GameObject blockParticle;

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
        if (gameObject.tag == "Player")
        {
            PlayerInfo player = NumericalManager.instance.GetMainPlayer();

            sm.ATK = player.fAtk;

            sm.playerHP.SetCurrentHP(player.fPlayerHp);
            sm.playerHP.SetMaxHp(player.fPlayerMaxHp);
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
        else if (sm.isDefense) //防禦狀態
        {
            Blocked();
            Instantiate(blockParticle, transform.position + new Vector3(0, bm.bloodFXPositionHieght * 0.6f, 0), Quaternion.identity);
        }
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

            _CheckHit(targetWc);

            if (sm.playerHP.HP < 0)
            {
                if (ac.CheckState("die"))
                {
                    
                }
                else
                {
                    Die();

                    if (gameObject.tag == "Player")
                    {
                        StartCoroutine(DeadDelay());
                    }
                    else if (gameObject.tag == "Npc")
                    {
                        NumericalManager.instance.GetExp(transform.GetSiblingIndex());
                    }
                }
                //Already dead
            }
            else
            {

                if (sm.playerHP.HP > 0)
                {
                    if (doHitAnimation)
                    {
                        Hit();
                    }
                    //do some VFX, like splatter blood...

                    Instantiate(bloodParticle, transform.position + new Vector3(0, bm.bloodFXPositionHieght * 0.6f, 0), Quaternion.identity);                    
                }
            }

            PlayerUI.UIManager.instance.HitPlayer(gameObject);
        }  
    }

    /// <summary>
    /// 檢查被打到的是誰
    /// </summary>
    private void _CheckHit(WeaponController targetWc)
    {
        PlayerInfo enemy;

        if (gameObject.tag == "Npc")
        {
            enemy = NumericalManager.instance.GetNpc(transform.GetSiblingIndex());
            enemy.fPlayerHp -= targetWc.GetATK();
            sm.playerHP.SetCurrentHP(enemy.fPlayerHp);
        }
        else if (gameObject.tag == "Player")
        {
            enemy = NumericalManager.instance.GetMainPlayer();
            enemy.fPlayerHp -= targetWc.GetATK();
        }
        
    }


    public IEnumerator DeadDelay()
    {
        yield return new WaitForSeconds(1.0f);
        NumericalManager.instance.ScenceFadeOut();
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
