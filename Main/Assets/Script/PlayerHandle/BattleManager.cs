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
    {
        //Debug.Log(col.name);
        if (col.tag == "Weapon")
        {
            am.DoDamage();
        }
    }
}
