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

            //rotationTime -= Time.fixedDeltaTime;

            //if (rotationTime <= 0)
            //{
            //    rotationTime = 5.0f;

            //    transform.forward = (-dev);
            //}


            if (spiderAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                if (dis < 7f)
                {
                    spiderAnim.SetBool("walk", false);
                    transform.forward = (-dev);
                    spiderAnim.SetTrigger("attack");
                }
                else
                {
                    spiderAnim.ResetTrigger("attack");
                    spiderAnim.SetBool("walk", true);
                }

            }
            else if (spiderAnim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                if (dis < 7f)
                {
                    spiderAnim.SetBool("walk", false);
                    spiderAnim.SetTrigger("attack");
                }
            }
            else if (spiderAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                spiderAnim.ResetTrigger("attack");
            }


        }
    }


}
