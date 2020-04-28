using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public float MaxHP;
    public float HP;   

    public void SetHP(float _MaxHP, float _HP)
    {
        MaxHP = _MaxHP;
        HP = _HP;
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
