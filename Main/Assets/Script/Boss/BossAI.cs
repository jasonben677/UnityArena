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

    public bool isDead = false;

    [System.NonSerialized]
    public bool isGetExp = false;

    [SerializeField] WeaponData mouseWeaponData;

    bool canTriggerAngry = true;

    Rigidbody myrigi;


    int attackIndex = -1;

    float AttackDelay = 0.5f;
    float walkDelay = 0f;

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

            if (NumericalManager.instance.GetBoss().fPlayerHp <= 0)
            {
                Debug.Log("dead");
                BossAnim.Play("Dying_A");
                if (!PlayerUI.UIManager.instance.winGame)
                {
                    StartCoroutine(BossDisappear());
                    PlayerUI.UIManager.instance.winGame = true;
                }

            }
            else if (NumericalManager.instance.GetBoss().fPlayerHp > 0)
            {
                if (dis >= 4.5f)
                {
                    walkDelay -= Time.deltaTime;

                    if (walkDelay <= 0)
                    {
                        BossAnim.Play("Run", 0);
                        myrigi.position += dev * 10.0f * Time.deltaTime;

                        AttackDelay = 0;
                        attackIndex = 3;
                  
                    }

                    transform.forward = dev;
                    attackEnable.AttackDisable();

                }
                else
                {
                    AttackDelay -= Time.deltaTime;

                    if (AttackDelay <= 0)
                    {
                        
                        transform.forward = dev;

                        switch (attackIndex)
                        {
                            case 0:
                                //normal attack
                                _AttackStateControl("adfadf", 2.5f, 3.0f, 2.0f);
                                break;

                            case 1:
                                //combo
                                _AttackStateControl("rgdasfa", 2.5f, 3.0f, 3.5f);
                                break;

                            case 2:
                                //anger
                                _AttackStateControl("anger", 2.5f, 3.0f, 3.0f);
                                break;

                            case 3:
                                //idle
                                _AttackStateControl("COMBAT_Mode", 0.6f, 0.8f, 1.0f);
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
        mouseWeaponData.ATK = 20;
        canTriggerAngry = false;
    }

    private void _AttackStateControl(string _name, float _attackTimeMin, float _attackTimeMax, float _walkDelay)
    {
        attackIndex = canTriggerAngry ? Random.Range(0, 3) : Random.Range(0, 2);
        AttackDelay = Random.Range(_attackTimeMin, _attackTimeMax);
        walkDelay = _walkDelay;

        BossAnim.speed = Random.Range(0.8f, 1.2f);
        BossAnim.Play(_name);

        if (_name == "anger")
        {
            fire01.gameObject.SetActive(true);
            fire02.gameObject.SetActive(true);
            canTriggerAngry = false;
        }

    }


    private IEnumerator BossDisappear()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }
}
