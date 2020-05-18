using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public bool isBossFight = false;
    public Collider wall;

    private void Start()
    {
        wall = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!isBossFight && other.tag == "Player")
        {
            isBossFight = true;

            Debug.Log("開始戰鬥");
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (isBossFight && other.tag == "Player")
        {
            wall.isTrigger = false;
        }
    }

}
