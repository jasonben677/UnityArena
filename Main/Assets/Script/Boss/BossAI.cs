using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : PlayerInput
{
    public GameObject player;
    public BossTrigger bossTrigger;
    public Animator spiderAnim;

    private void Awake()
    {
        NumericalManager.instance.SetBoss();
    }

    private void FixedUpdate()
    {
        if (bossTrigger.isBossFight)
        {
            float dis = Vector3.Distance(transform.position, player.transform.position);
            Vector3 dev = (player.transform.position - transform.position).normalized;
            if (dis < 12.5f)
            {
                Debug.Log("attack");
                spiderAnim.SetBool("walk", false);
                spiderAnim.SetBool("attack", true);
            }
            else
            {
                dev.y = 0;
                transform.position += dev * 5.0f * Time.fixedDeltaTime;
                transform.forward = (-dev);

                spiderAnim.SetBool("attack", false);
                spiderAnim.SetBool("walk", true);
            }
        }
    }


}
