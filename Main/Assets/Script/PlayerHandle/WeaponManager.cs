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
    public WeaponController wcR;

    // Start is called before the first frame update
    void Start()
    {
        whR = transform.DeepFind("weaponHandleR").gameObject;
        //whL = transform.DeepFind("weaponHandleL").gameObject;
        
        weaponColR = whR.GetComponentInChildren<Collider>();
        //weaponColL = whL.GetComponentInChildren<Collider>();

        wcR = whR.GetComponent<WeaponController>();
    }

    public void UpdateWeaponCollider(Collider col)
    {
        weaponColR = col;
    }

    public void UnLoadWeapon()
    {
        foreach (Transform tran in whR.transform)
        {
            weaponColR = null;
            wcR.wdata = null;
            Destroy(tran.gameObject);
        }
    }

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

    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }

    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }
}
