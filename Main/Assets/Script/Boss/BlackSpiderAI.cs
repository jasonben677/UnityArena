using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSpiderAI : PlayerInput
{
    public Animation animation;
    public Collider attackCol;
    public BossTrigger bossTrigger;
    public Transform player;

    public GameObject bossBlock;

    public bool isDead = false;

    [System.NonSerialized]
    public bool isGetExp = false;

    float AttackDelay = 0.5f;
    float walkDelay = 0f;

    float walkTime = 1.0f;

    int walkIndex = 0;
    int attackIndex = -1;

    void Awake()
    {
        NumericalManager.instance.SetSpider();
        bossBlock.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossTrigger.isBossFight)
        {
            float dis = Vector3.Distance(transform.position, player.position);
            Vector3 dev = (player.position - transform.position).normalized;
            dev.y = 0;
            //Debug.LogError(dis);
            if (NumericalManager.instance.GetSpider().fPlayerHp <= 0)
            {
                if (!isDead)
                {
                    _PlayAnimation("deathNormal");
                    Debug.Log("dead");
                    attackCol.enabled = false;
                    bossTrigger.wall.isTrigger = true;
                    bossBlock.SetActive(false);
                    StartCoroutine(BossDissapear());
                    isDead = true;
                }


            }
            else if (NumericalManager.instance.GetSpider().fPlayerHp > 0)
            {
                if (dis > 4.0f)
                {
                    walkDelay -= Time.deltaTime;

                    if (walkDelay <= 0)
                    {
                        switch (walkIndex)
                        {
                            case 0:
                                _WalkForward(dev);
                                break;

                            case 1:
                                _Idle(dev);
                                break;

                            default:
                                break;
                        }

                        AttackDelay = 0;
                        transform.forward = (-dev);
                        attackIndex = 3;
                    }


                }
                else
                {
                    AttackDelay -= Time.deltaTime;
                    if (AttackDelay <= 0)
                    {        
                        
                        transform.forward = (-dev);
                        switch (attackIndex)
                        {
                            case 0:
                                _AttackStateControl("biteAggressive", 2.0f, 2.5f);
                                break;

                            case 1:
                                _AttackStateControl("3HitComboAggressive", 2.0f, 3.0f);
                                break;

                            case 2:
                                _AttackStateControl("jumpBiteAggressive", 2.0f, 2.5f);
                                break;

                            case 3:
                                _AttackStateControl("idleNormal1", 0.6f, 0.8f);
                                break;

                            default:
                                break;
                        }
                   
                    }
                }
            }
        }
        else
        {
            if (NumericalManager.instance.GetSpider().fPlayerHp > 0)
            {
                _PlayAnimation("idleNormal1");
            }
        }
    }


    private void _PlayAnimation(string _name)
    {
        try
        {
            animation[_name].speed = Random.Range(0.6f, 1.2f);
            animation.Play(_name);
        }
        catch (System.Exception)
        {

            Debug.LogError(_name);
        }

    }

    private void _AttackStateControl(string _name, float _attackTimeMin, float _attackTimeMax)
    {
        try
        {
            AttackDelay = Random.Range(_attackTimeMin, _attackTimeMax);
            animation[_name].speed = Random.Range(0.8f, 1.2f);
            animation.Play(_name);
            attackIndex = Random.Range(0, 3);
            walkDelay = 1.0f;
        }
        catch (System.Exception)
        {

            Debug.LogError(_name);
        }
    }


    private void _PlayAnimNormalSpeed(string _name)
    {
        try
        {
            animation.Play(_name);
        }
        catch (System.Exception)
        {

            Debug.LogError(_name);
        }



    }

    private void _WalkForward(Vector3 dev)
    {

        walkTime -= Time.deltaTime;

        transform.position += dev * 5.0f * Time.deltaTime;
        _PlayAnimNormalSpeed("walkNormal");
        Debug.Log("Walk");
        if (walkTime <= 0)
        {
            walkTime = 1;
            //walkIndex = Random.Range(0, 2);
        }
    }

    private void _Idle(Vector3 dev)
    {
        walkTime -= Time.deltaTime;
        _PlayAnimNormalSpeed("idleNormal1");

        if (walkTime <= 0)
        {
            walkTime = 1;
            walkIndex = Random.Range(0, 2);
        }
    }

    private IEnumerator BossDissapear()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }


}
