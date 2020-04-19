using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public ActorManager am;
    private Collider weaponColR; //colliderRight
    //private Collider weaponColL; //colliderLeft

    public GameObject whR; //weaponHandleRight
    //public GameObject whL;

    // Start is called before the first frame update
    void Start()
    {
        whR = transform.DeepFind("weaponHandleR").gameObject;
        //whL = transform.DeepFind("weaponHandleL").gameObject;
        
        weaponColR = whR.GetComponentInChildren<Collider>();
        //weaponColL = whL.GetComponentInChildren<Collider>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public void WeaponEnable()
    {
        weaponColR.enabled = true;
        //Debug.Log("WeaponEnable");
    }

    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        //Debug.Log("WeaponDisable");
    }
}
