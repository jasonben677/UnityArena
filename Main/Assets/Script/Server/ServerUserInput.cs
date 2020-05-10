using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerUserInput : PlayerInput
{

    public StateManager stateManager;

    Animator anim;

    int myIndex = -1;
    float nextHp = 0;
    float nextAtk = 0;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateHpAndATK();
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


    public void UpdatePlayerState(int _index, float _hp, float _atk)
    {
        myIndex = _index;
        nextHp = _hp;
        nextAtk = _atk;
        Debug.Log( transform.name + " HP = " + _hp);
        UpdateHpAndATK();

    }

    public void UpdateHpAndATK()
    {
        if (myIndex > -1)
        {
            if (stateManager.playerHP == null)
            {
                Debug.Log("還沒生成hp");
                return;
            }
            stateManager.ATK = nextAtk;
            stateManager.playerHP.SetCurrentHP(nextHp);
            //Debug.Log(gameObject.name + " hp: " + stateManager.playerHP.HP + " atk : " + stateManager.ATK);
        }
    }

}
