using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSpiderAI : PlayerInput
{
    public Animation animation;
    public Collider attackCol;
    public BossTrigger bossTrigger;
    public Transform player;

    bool beginDeath;
    float times = 0.5f;

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
                if (dis > 5.0f)
                {
                    transform.position += dev * 5.0f * Time.deltaTime;
                    transform.forward = (-dev);
                    times = Random.Range(0.3f, 0.6f);
                    _PlayAnimation("walkNormal");
                }
                else
                {
                    times -= Time.deltaTime;
                    if (times <= 0)
                    {
                        times = Random.Range(1.2f, 1.6f);
                        transform.forward = (-dev);
                        _PlayAnimation("biteAggressive");
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
        animation.Play(_name);
    }
}
