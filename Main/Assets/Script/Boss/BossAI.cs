using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : PlayerInput
{
    public GameObject player;
    public BossTrigger bossTrigger;
    public Animator spiderAnim;
    private Vector3 originPos;

    private float waitTime = 0.8f;
    private float rotationTime = 5.0f;


    private void Awake()
    {
        NumericalManager.instance.SetBoss();
        originPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (bossTrigger.isBossFight)
        {
            float dis = Vector3.Distance(transform.position, player.transform.position);
            Vector3 dev = (player.transform.position - transform.position).normalized;
            dev.y = 0;
            float degree = Vector3.Dot(dev, transform.forward);

            rotationTime -= Time.fixedDeltaTime;
            if (rotationTime <= 0)
            {
                rotationTime = 5.0f;

                transform.forward = (-dev);
            }



            if (dis < 7f)
            {
                spiderAnim.SetBool("walk", false);              
                rotationTime -= Time.fixedDeltaTime;

                if (degree <= -0.9)
                {
                    waitTime -= Time.fixedDeltaTime;
                    Debug.Log("CanAttack");

                    if (waitTime <= 0)
                    {
                        waitTime = Random.Range(1.0f, 1.2f);
                        spiderAnim.SetBool("attack", true);
                    }
                    else
                    {
                        spiderAnim.SetBool("attack", false);
                    }

                }
                else
                {
                    spiderAnim.SetBool("attack", false);
                }

            }
            else
            {
                if (dis > 8.0f)
                {
                    transform.forward = (-dev);
                }

                transform.position += dev * 5.0f * Time.fixedDeltaTime;
                spiderAnim.SetBool("attack", false);
                spiderAnim.SetBool("walk", true);

            }
        }
        else
        {
            float dis = Vector3.Distance(transform.position, originPos);
            Vector3 dev = (originPos - transform.position).normalized;
            dev.y = 0;
            if (dis > 0.5f)
            {

                transform.position += dev * 5.0f * Time.fixedDeltaTime;
                transform.forward = (-dev);

                spiderAnim.SetBool("attack", false);
                spiderAnim.SetBool("walk", true);
            }
            else
            {
                spiderAnim.SetBool("attack", false);
                spiderAnim.SetBool("walk", false);
            }


        }
    }


}
