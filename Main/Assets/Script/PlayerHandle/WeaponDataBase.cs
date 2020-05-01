using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase
{
    private string weaponDatabaseFileName = "weaponData";
    public readonly JSONObject weaponDataBase;

    public WeaponDataBase()
    {
        TextAsset weaponContent = Resources.Load(weaponDatabaseFileName) as TextAsset;
        weaponDataBase = new JSONObject(weaponContent.text);
        //Debug.Log(weaponDataBase["katana01"]["ATK"].f);
        //Debug.Log(weaponDataBase["katana01"]["ATK"].str);
        //Debug.Log(weaponDataBase["katana01"]["ATK"].ToString());
    }
}
