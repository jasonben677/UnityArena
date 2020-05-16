using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongNpc : MonoBehaviour
{

    private AITest[] myStrongNpc;


    private void Awake()
    {
        _GetAllStrongNpc();
    }

    // Update is called once per frame
    void Update()
    {
        _StrongNpcUpdate();
    }


    private void _StrongNpcUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (myStrongNpc[i].gameObject.activeSelf == true)
            {
                myStrongNpc[i].NpcUpdate();
            }

        }
    }

    private void _GetAllStrongNpc()
    {
        myStrongNpc = new AITest[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            myStrongNpc[i] = transform.GetChild(i).GetComponent<AITest>();
        }
        NumericalManager.instance.SetStrongNpc(transform.childCount);
    }
}
