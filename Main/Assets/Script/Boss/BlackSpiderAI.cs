using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSpiderAI : PlayerInput
{
    public Animation animation;
    public Collider attackCol;
    public BossTrigger bossTrigger;
    public Transform player;

    float AttackDelay = 0.5f;
    float walkTime = 1.0f;
    int walkIndex = 0;

    private void Awake()
    {
       
    }

    void Start()
    {
        NumericalManager.instance.SetBoss();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossTrigger.isBossFight)
        {
            float dis = Vector3.Distance(transform.position, player.position);
            Vector3 dev = (player.position - transform.position).normalized;
            dev.y = 0;

            if (NumericalManager.instance.GetBoss().fPlayerHp <= 0 && NumericalManager.instance.GetBoss().fPlayerHp >= -10)
            {
                _PlayAnimation("deathNormal");
                Debug.Log("dead");
                attackCol.enabled = false;
                NumericalManager.instance.GetBoss().fPlayerHp = -15;
            }
            else if (NumericalManager.instance.GetBoss().fPlayerHp > 0)
            {
                if (dis > 3.5f)
                {
                    switch (walkIndex)
                    {
                        case 0:
                            _WalkForward(dev);
                            break;

                        case 1:
                            _WalkBack(dev);
                            break;

                        default:
                            break;
                    }

                    AttackDelay = Random.Range(1.0f, 1.2f);

                    //transform.position += dev * 5.0f * Time.deltaTime;
                    transform.forward = (-dev);
                    
                    //_PlayAnimNormalSpeed("walkNormal");
                }
                else
                {
                    AttackDelay -= Time.deltaTime;
                    if (AttackDelay <= 0)
                    {
                        AttackDelay = Random.Range(2.0f, 3.0f);
                        transform.forward = (-dev);
                        int attackIndex = Random.Range(0, 4);

                        switch (attackIndex)
                        {
                            case 0:
                                _PlayAnimation("biteAggressive");
                                break;

                            case 1:
                                _PlayAnimation("3HitComboAggressive");
                                break;

                            case 2:
                                _PlayAnimation("jumpBiteNormal");
                                break;

                            case 3:
                                _PlayAnimation("jumpBiteAggressive");
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
            if (NumericalManager.instance.GetBoss().fPlayerHp > 0)
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

        if (walkTime <= 0)
        {
            walkTime = 1;
            walkIndex = Random.Range(0, 2);
        }
    }

    private void _WalkBack(Vector3 dev)
    {
        walkTime -= Time.deltaTime;
        _PlayAnimNormalSpeed("idleNormal1");

        if (walkTime <= 0)
        {
            walkTime = 1;
            walkIndex = Random.Range(0, 2);
        }
    }



}
