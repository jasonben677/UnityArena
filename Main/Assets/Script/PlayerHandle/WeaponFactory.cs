using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory 
{
    private WeaponDataBase weaponDB;

	public WeaponFactory(WeaponDataBase _weaponDB)
	{
		weaponDB = _weaponDB;
		//Debug.Log("weaponFactory is initialized");
	}

	public GameObject CreateWeapon(string weaponName, Vector3 position, Quaternion rotation)
	{
		GameObject prefab = Resources.Load(weaponName) as GameObject;
		GameObject obj =  GameObject.Instantiate(prefab, position, rotation);

		WeaponData wData = obj.AddComponent<WeaponData>();
		wData.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;

		return obj;
	}

	public Collider CreateWeapon(string weaponName, WeaponManager wm)
	{
		//WeaponController wc;
		//wc = wm.wcR;

		GameObject prefab = Resources.Load(weaponName) as GameObject;
		GameObject obj = GameObject.Instantiate(prefab);
		obj.transform.parent = wm.whR.transform;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.identity;

		WeaponData wData = obj.AddComponent<WeaponData>();
		wData.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;
		//wc.wdata = wData;
		wm.wcR.wdata = wData;

		return obj.GetComponent<Collider>();
	}	
}
