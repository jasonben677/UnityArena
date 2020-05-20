using DG.Tweening;
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
    [SerializeField]
    private GameObject counterBackParticle;

    [Header("==== Audio ====")]
    [SerializeField]
    private AudioClip katanaSound;
    [SerializeField]
    private AudioClip bloodSound;

    [Header("==== Material ====")]
    [SerializeField]
    private Material dead;
    private SkinnedMeshRenderer[] smrList;

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

        smrList = transform.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
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
            Instantiate(counterBackParticle, targetWc.wm.transform.position + new Vector3(0, targetWc.wm.am.bm.bloodFXPositionHieght * 0.6f, 0), Quaternion.identity);
            //Instantiate(counterBackParticle, wm.whR.transform.position, Quaternion.identity);
        }
        else if (sm.isCounterBackFailure) //反擊失敗
        {
            Instantiate(counterBackParticle, wm.whR.transform.position, Quaternion.identity);

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
            AudioSource.PlayClipAtPoint(katanaSound, wm.whR.transform.position, 1);            
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

            if (sm.playerHP.HP <= 0)
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
                    else if (gameObject.tag == "Boss")
                    {
                        BossAI bossAI = GetComponent<BossAI>();

                        if (!bossAI.isGetExp)
                        {
                            NumericalManager.instance.GetExp(0, 0);
                            bossAI.isGetExp = true;
                        }                      
                        //Boss死後，切換回原來的BGM
                        SwitchBGM.instance.audioSource.clip = SwitchBGM.instance.audioClips[0];
                        SwitchBGM.instance.audioSource.Play();
                    }
                    else if (gameObject.tag == "StrongNpc")
                    {
                        NumericalManager.instance.GetExp(transform.GetSiblingIndex(), 1);
                    }
                    else if (gameObject.tag == "Spider")
                    {
                        BlackSpiderAI blackSpiderAI = GetComponent<BlackSpiderAI>();

                        NumericalManager.instance.GetExp(0, 2);
                        //Spider死後，切換回原來的BGM
                        SwitchBGM.instance.audioSource.clip = SwitchBGM.instance.audioClips[0];
                        SwitchBGM.instance.audioSource.Play();
                    }
                    else if (gameObject.tag == "Npc")
                    {
                        NumericalManager.instance.GetExp(transform.GetSiblingIndex(), 3);
                    }
                    
                    if (gameObject.tag == "Npc" || gameObject.tag == "StrongNpc") 
                    {
                        foreach (var smr in smrList)
                        {
                            //GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
                            smr.material = dead;
                            //smr.material.DOFloat(1f, "_Step", 2.5f).OnComplete(() => Destroy(clone));
                            smr.material.DOFloat(1f, "_Step", 3.5f);
                        }
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
                    //Instantiate(bloodParticle, transform.position + new Vector3(0, bm.bloodFXPositionHieght * 0.6f, 0), Quaternion.identity);
                    Instantiate(bloodParticle, transform.position + new Vector3(0, bm.bloodFXPositionHieght * 0.6f, 0), Quaternion.Euler(-90f, 0f, 0f));
                    AudioSource.PlayClipAtPoint(bloodSound, transform.position + new Vector3(0, bm.bloodFXPositionHieght * 0.6f, 0), 0.5f);
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
            float atk = targetWc.GetATK();

            if (enemy.fPlayerHp >= 0)
            {
                PlayerUI.UIManager.instance.ShowAttack(gameObject, atk);
            }

            enemy.fPlayerHp -= atk;
            sm.playerHP.SetCurrentHP(enemy.fPlayerHp);

        }
        else if (gameObject.tag == "Player")
        {
            enemy = NumericalManager.instance.GetMainPlayer();
            float atk = targetWc.GetATK();

            if (enemy.fPlayerHp >= 0)
            {
                PlayerUI.UIManager.instance.ShowAttack(gameObject, atk);
            }

            enemy.fPlayerHp -= atk;
            sm.playerHP.SetCurrentHP(enemy.fPlayerHp);
        }
        else if (gameObject.tag == "StrongNpc")
        {
            enemy = NumericalManager.instance.GetStrongNpc(transform.GetSiblingIndex());
            float atk = targetWc.GetATK();

            if (enemy.fPlayerHp >= 0)
            {
                PlayerUI.UIManager.instance.ShowAttack(gameObject, atk);
            }

            enemy.fPlayerHp -= atk;
            sm.playerHP.SetCurrentHP(enemy.fPlayerHp);
        }
        else if (gameObject.tag == "Spider")
        {
            enemy = NumericalManager.instance.GetSpider();
            float atk = targetWc.GetATK();

            if (enemy.fPlayerHp >= 0)
            {
                PlayerUI.UIManager.instance.ShowAttack(gameObject, atk);
            }

            enemy.fPlayerHp -= atk;
            sm.playerHP.SetCurrentHP(enemy.fPlayerHp);
        }
        else if (gameObject.tag == "Boss")
        {
            enemy = NumericalManager.instance.GetBoss();
            float atk = targetWc.GetATK();

            if (enemy.fPlayerHp >= 0)
            {
                PlayerUI.UIManager.instance.ShowAttack(gameObject, atk);
            }

            enemy.fPlayerHp -= atk;
            sm.playerHP.SetCurrentHP(enemy.fPlayerHp);
        }

        
    }


    public IEnumerator DeadDelay()
    {
        yield return new WaitForSeconds(1.0f);    
        NumericalManager.instance.ScenceFadeOut(2);
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
