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

    private GameObject objFXA; //普攻A刀光特效
    private GameObject objFXB; //普攻B刀光特效

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

    public GameObject OnFXEnableA()
    {
        //Debug.Log("On FXEnable A");
        GameObject prefabFX = Resources.Load("FX001") as GameObject;
        objFXA = GameObject.Instantiate(prefabFX);
        objFXA.transform.parent = whR.transform;
        objFXA.transform.localPosition = Vector3.zero;
        objFXA.transform.localRotation = Quaternion.identity;

        return objFXA;
    }

    public void OnFXDisableA()
    {
        //Debug.Log("On FXDisable A");
        Destroy(objFXA.gameObject);
    }

    public GameObject OnFXEnableB()
    {
        GameObject prefabFX = Resources.Load("FX002") as GameObject;
        objFXB = GameObject.Instantiate(prefabFX);
        objFXB.transform.parent = whR.transform;
        objFXB.transform.localPosition = Vector3.zero;
        objFXB.transform.localRotation = Quaternion.identity;

        return objFXB;
    }

    public void OnFXDisableB()
    {
        Destroy(objFXB.gameObject);
    }
}
