using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public bool isBossFight = false;


    private void OnTriggerEnter(Collider other)
    {
        if (isBossFight)
        {
            isBossFight = false;
            Debug.Log("離開戰鬥");
        }
        else
        {
            isBossFight = true;
            Debug.Log("開始戰鬥");
        }
    }

}
