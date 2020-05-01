using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager wm;
    public WeaponData wdata;

    // Start is called before the first frame update
    void Start()
    {
        wdata = GetComponentInChildren<WeaponData>();
    }

    public float GetATK()
    {
        return wdata.ATK + wm.am.sm.ATK;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
