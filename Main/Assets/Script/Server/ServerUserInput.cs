using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerUserInput : PlayerInput
{
    Animator anim;
    HealthPoint health;
    StateManager stateManager;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SetDir(Vector3 _dir)
    {
        transform.forward = _dir;
    }

    public void SetAnim(float _walk, bool _attack, Vector3 _pos)
    {
        if (anim == null) { return; }

        //float curWalk = Mathf.Lerp(anim.GetFloat("forward"), _walk, 0.75f);

        float dis = (_pos - transform.position).magnitude;

        //_walk = (_walk > 0.05f) ? 1.0f : 0f;

        if (dis < 0.5f)
        {
            anim.SetFloat("forward", 0);
        }
        else
        {
            anim.SetFloat("forward", 1);
        }


        if (_attack)
        {
            anim.SetTrigger("attack");
        }
        
    }


    public void UpdatePlayerState(float _hp, float _atk)
    {
        //if (health == null)
        //{
        //    health = GetComponent<HealthPoint>();
        //}
        //else
        //{
        //    health.HP = _hp;
        //}


        //if (stateManager == null)
        //{
        //    stateManager = GetComponent<StateManager>();
        //}
        //else
        //{
        //    stateManager.ATK = _atk;
        //}
        
    }

}
