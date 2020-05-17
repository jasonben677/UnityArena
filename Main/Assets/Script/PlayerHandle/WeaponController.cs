using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager wm;
    public WeaponData wdata;
    public static bool weaponLevelUp = false;
    private float initialATK;

    // Start is called before the first frame update
    void Start()
    {
        wdata = GetComponentInChildren<WeaponData>();
        initialATK = wdata.ATK;
    }   

    public float GetATK()
    {
        if (weaponLevelUp == false)
        {
            wdata.ATK = initialATK;
        }
        else 
        {
            //Debug.Log("weaponLevelUp!!!!!");
            wdata.ATK = initialATK * 1.3f;
        }
        float totalATK = Mathf.Round((wdata.ATK + wm.am.sm.ATK) * Random.Range(0.2f, 1f));          
        return totalATK;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
