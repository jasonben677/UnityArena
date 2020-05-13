using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Collider attack;


    public void AttackEnable()
    {
        attack.enabled = true;
    }

    public void AttackDisable()
    {
        attack.enabled = false;
    }

}
