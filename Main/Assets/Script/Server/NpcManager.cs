using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    private TestDll.Message03 message;
    private AITest[] npcObjs;
    private float timer = 0;


    private void Awake()
    {
        _GetAllNpc();
    }


    private void FixedUpdate()
    {
        if (LoginManager.instance != null)
        {
            message = LoginManager.instance.client.tranmitter.mMessage;
            _UseServerData();
        }
        else
        {
            Debug.Log("UpdateNpc");
        }
    }


    private void _UseServerData()
    {
        if (message.isNpcServer == true)
        {
            timer += Time.fixedDeltaTime;
            if (timer > 0.25f)
            {
                _SendNpcData();
                timer = 0;
            }

            Debug.Log("I'm npc server");
            _RunNpcAI();
        }
        else
        {
            Debug.Log("UpdateNpc");
        }
    }


    private void _SendNpcData()
    {
        int counts = (message.myEnemy.Length <= npcObjs.Length) ? message.myEnemy.Length : npcObjs.Length;
        for (int i = 0; i < counts; i++)
        {
            message.myEnemy[i].name = "雜魚";
            message.myEnemy[i].maxHp = 100;
            message.myEnemy[i].hp = npcObjs[i].data.fHP;
        }
    }


    private void _RunNpcAI()
    {
        foreach (AITest npc in npcObjs)
        {
            //npc.NpcUpdate();
        }
    }

    private void _GetAllNpc()
    {
        npcObjs = new AITest[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            npcObjs[i] = transform.GetChild(i).GetComponent<AITest>();

            if (LoginManager.instance != null)
            {
                LoginManager.instance.client.tranmitter.mMessage.myEnemy[i] = new TestDll.Player();
            }
        }
    }

}
