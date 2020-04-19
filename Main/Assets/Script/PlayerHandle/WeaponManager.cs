using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Collider weaponCol;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    public void WeaponEnable()
    {
        weaponCol.enabled = true;
        //Debug.Log("WeaponEnable");
    }

    public void WeaponDisable()
    {
        weaponCol.enabled = false;
        //Debug.Log("WeaponDisable");
    }
}
