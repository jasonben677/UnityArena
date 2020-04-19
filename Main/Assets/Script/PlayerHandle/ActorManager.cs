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
        
    }

    // Update is called once per frame
    void Update()
    {
        sm.Test();
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid)
    {
        if (sm.isImmortal)
        {
            //Do nothing
        }
        else
        {
            if (attackValid)
            {
                HitOrDie(true);
            }            
        }                      
    }

    public void HitOrDie(bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {
            //Already dead
        }
        else
        {
            sm.AddHP(-5);
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                }
                //do some VFX, like splatter blood...
            }
            else
            {
                Die();
            }
        }
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
