using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager wm;
    public WeaponData wdata;
    public static bool weaponLevelUp = false;    

    // Start is called before the first frame update
    void Start()
    {
        wdata = GetComponentInChildren<WeaponData>();
    }   

    public float GetATK()
    {
        if (weaponLevelUp == false)
        {
            wdata.ATK *= 1f;
        }
        else 
        {
            //Debug.Log("weaponLevelUp!!!!!");
            wdata.ATK *= 1.5f;
        }
        float totalATK = Mathf.Round((this.wdata.ATK + wm.am.sm.ATK) * Random.Range(0.2f, 1f));          
        return totalATK;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
