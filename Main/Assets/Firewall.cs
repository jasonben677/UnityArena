using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    public GameObject GuanZhu;

     bool isBossDie;
    bool isBossDead;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         isBossDie= GuanZhu.GetComponent<StateManager>().isDie;
        isBossDead = GuanZhu.GetComponent<BossAI>().isDead;
        if (isBossDie == true || isBossDead == true) ;
        {
            gameObject.SetActive(true);
        }
    }
}
