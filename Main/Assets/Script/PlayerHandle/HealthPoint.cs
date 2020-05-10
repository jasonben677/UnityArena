using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public float MaxHP
    {
        get;
        private set;
    }

    public float HP 
    {
        get;
        private set;
    }

    public void SetHP(float _MaxHP, float _HP)
    {
        MaxHP = _MaxHP;
        HP = _HP;
    }

    public void SetMaxHp(float _MaxHP)
    {
        MaxHP = _MaxHP;
    }

    public void SetCurrentHP(float _hp)
    {
        HP = _hp;
    }

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, MaxHP);
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
