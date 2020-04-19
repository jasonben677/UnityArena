using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public ActorManager am;

    public float HP = 15.0f;

    //private void Awake()
    //{
    //    am = gameObject.GetComponent<ActorManager>();
    //}

    public void Test()
    {
        Debug.Log("sm test: HP is" + HP);
    }
}    
