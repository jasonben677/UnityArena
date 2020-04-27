using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour
{
    public ActorManager am;

    private CapsuleCollider defenseCol;
    //Start is called before the first frame update
    void Start()
    {
        defenseCol = GetComponent<CapsuleCollider>();
        defenseCol.center = Vector3.up * 1.0f;
        defenseCol.height = 2.0f;
        defenseCol.radius = 0.3f;
        defenseCol.isTrigger = true;

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider col)
    {//Debug.Log(col.name);
        WeaponController targetWc = col.GetComponentInParent<WeaponController>();

        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.gameObject;

        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - receiver.transform.position;

        float attackingAngle1 = Vector3.Angle(attacker.transform.forward, attackingDir);
        float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
        float counterAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward); //should be close to 180 degrees

        bool attackValid = (attackingAngle1 < 60);
        bool counterValid = (counterAngle1 < 90 && Mathf.Abs(counterAngle2 - 180) < 180);

        if (col.tag == "Weapon")
        {
            //if(attackingAngle1 <= 45)
            //{
            //    am.TryDoDamage(targetWc);
            //}
            am.TryDoDamage(targetWc, attackValid, counterValid);         
        }
    }
}
