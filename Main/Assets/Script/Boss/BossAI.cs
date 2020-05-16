using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : PlayerInput
{
    public Transform player;
    public BossTrigger bossTrigger;
    public Animator BossAnim;
    public BossAttack attackEnable;

    public ParticleSystem fire01;
    public ParticleSystem fire02;


    bool canTriggerAngry = true;
    Rigidbody myrigi;
    float AttackDelay = 0.5f;


    private void Awake()
    {
        NumericalManager.instance.SetBoss();
        myrigi = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (bossTrigger.isBossFight)
        {
            float dis = Vector3.Distance(transform.position, player.position);
            Vector3 dev = (player.position - transform.position).normalized;
            dev.y = 0;
            //Debug.Log(dis);

            if (NumericalManager.instance.GetBoss().fPlayerHp <= 0 && NumericalManager.instance.GetBoss().fPlayerHp >= -10)
            {
            
                Debug.Log("dead");
                BossAnim.Play("Dying_A");
                NumericalManager.instance.GetBoss().fPlayerHp = -15;
            }
            else if (NumericalManager.instance.GetBoss().fPlayerHp > 0)
            {
                if (dis > 5.5f)
                {
                    BossAnim.Play("Run", 0);
                    myrigi.position += dev * 10.0f * Time.deltaTime;

                    AttackDelay = Random.Range(1.0f, 1.2f);

                    //transform.position += dev * 5.0f * Time.deltaTime;
                    transform.forward = dev;
                    attackEnable.AttackDisable();
                    //_PlayAnimNormalSpeed("walkNormal");
                }
                else
                {
                    AttackDelay -= Time.deltaTime;

                    if (AttackDelay <= 0)
                    {
                        AttackDelay = Random.Range(2.0f, 3.0f);
                        transform.forward = dev;
                        int attackIndex = canTriggerAngry ? Random.Range(0,5) : Random.Range(0,4);

                        switch (attackIndex)
                        {
                            case 0:
                                _NormalAttack();
                                break;

                            case 1:
                                _NormalAttack();
                                break;

                            case 2:
                                _ComboAttack();
                                break;

                            case 3:
                                _NormalAttack();
                                break;

                            case 4:
                                _GetAngry();
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
               
            }
        }
    }

    private void _NormalAttack()
    {
        BossAnim.speed = Random.Range(0.8f, 1.2f);
        BossAnim.Play("adfadf");
    }

    private void _ComboAttack()
    {
        BossAnim.speed = Random.Range(0.8f, 1.2f);
        BossAnim.Play("rgdasfa");
    }

    private void _GetAngry()
    {
        BossAnim.Play("anger");
        fire01.gameObject.SetActive(true);
        fire02.gameObject.SetActive(true);
        canTriggerAngry = false;
    }
}
