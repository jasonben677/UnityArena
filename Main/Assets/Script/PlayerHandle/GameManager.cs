using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WeaponManager testWm;
    private static GameManager instance;
    private WeaponDataBase weaponDB;
    private WeaponFactory weaponFact;


    // Start is called before the first frame update
    void Awake()
    {
        CheckGameObject();
        CheckSingle();
    }

    private void Start()
    {
        InitWeaponDB();
        //Debug.Log(weaponDB.weaponDataBase["katana01"]);
        InitWeaponFactory();

        Collider col =  weaponFact.CreateWeapon("katana02", testWm);
        testWm.UpdateWeaponCollider(col);
    }

    // Update is called once per frame
    void Update()
    {
        OnSwitchWeapon();  
    }


    private void OnSwitchWeapon()    //切換武器功能
    {
        if (Input.GetKey(KeyCode.Z))
        {
            testWm.UnLoadWeapon();
            testWm.UpdateWeaponCollider(weaponFact.CreateWeapon("katana01", testWm));
        }
        if (Input.GetKey(KeyCode.X))
        {
            testWm.UnLoadWeapon();
            testWm.UpdateWeaponCollider(weaponFact.CreateWeapon("katana02", testWm));
        }
        if (Input.GetKey(KeyCode.C))
        {
            testWm.UnLoadWeapon();
            testWm.UpdateWeaponCollider(weaponFact.CreateWeapon("katana03", testWm));
        }
        if (Input.GetKey(KeyCode.V))
        {
            testWm.UnLoadWeapon();
        }
    }


    private void CheckGameObject()
    {
        if(tag == "GM")
        {
            return;
        }
        Destroy(this);
    }

    private void CheckSingle()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    private void InitWeaponDB()
    {
        weaponDB = new WeaponDataBase();
    }

    private void InitWeaponFactory()
    {
        weaponFact = new WeaponFactory(weaponDB);
    }
}
