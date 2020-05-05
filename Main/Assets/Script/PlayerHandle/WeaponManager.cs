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

    public float warpDuration = 0.25f;

    private GameObject objFXA; //普攻A刀光特效
    private GameObject objFXB; //普攻B刀光特效
    private GameObject objFXC; //普攻C刀光特效
    private Animator anim;    

    // Start is called before the first frame update
    void Start()
    {
        whR = transform.DeepFind("weaponHandleR").gameObject;
        //whL = transform.DeepFind("weaponHandleL").gameObject;
        
        weaponColR = whR.GetComponentInChildren<Collider>();
        //weaponColL = whL.GetComponentInChildren<Collider>();

        wcR = whR.GetComponent<WeaponController>();

        anim = transform.gameObject.GetComponent<Animator>();        
    }

    /// <summary>
    /// 替換武器時，更新武器collider的方法
    /// </summary>
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

    /// <summary>
    /// 武器開關
    /// </summary>
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

    /// <summary>
    /// 反擊成立開關
    /// </summary>
    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }

    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }

    /// <summary>
    /// 普攻刀光特效開關
    /// </summary>   
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

    public GameObject OnFXEnableC()
    {
        GameObject prefabFX = Resources.Load("FX003") as GameObject;
        objFXC = GameObject.Instantiate(prefabFX);
        objFXC.transform.parent = whR.transform;
        objFXC.transform.localPosition = Vector3.zero;
        objFXC.transform.localRotation = Quaternion.identity;

        return objFXC;
    }

    public void OnFXDisableC()
    {
        Destroy(objFXC.gameObject);
    }

    public void OnWarp()
    {
        //GameObject prefabModel = Resources.Load("Ninjia") as GameObject;
        //GameObject NinjiaModel = Instantiate(prefabModel);
        //NinjiaModel.transform.position = transform.position;
        //NinjiaModel.transform.rotation = transform.rotation;
        //Destroy(NinjiaModel.GetComponent<WeaponManager>().whR.gameObject);
        //Destroy(NinjiaModel.GetComponent<Animator>());
        //Destroy(NinjiaModel.GetComponent<WeaponManager>());

        //SkinnedMeshRenderer[] skinMeshList = NinjiaModel.GetComponentsInChildren<SkinnedMeshRenderer>();
        //foreach (SkinnedMeshRenderer smr in skinMeshList)
        //{
        //    smr.material = ; //shader
        //    smr.material. ;//DOTween....().OnComplete(()=>Destroy(NinjiaModel));
        //}

        RevealModel(false);
        anim.speed = 0;

        //DOTween...
        //transform.DOMOve(am.ac.camcon.lockTarget.transform.position, warpDuration).SetEase(Ease.InExpo).OnComplete(()=>FinishWarp());
        transform.position = Vector3.Lerp(transform.position, am.ac.camcon.lockTarget.transform.position, 0.5f);
        if(transform.position == am.ac.camcon.lockTarget.transform.position)
        {
            Debug.Log("FinishWarp Enter");
            FinishWarp();
        }

        //FX1.Play();
        //FX2.Play();...
    }

    private void FinishWarp()
    {
        RevealModel(true);

        //SkinnedMeshRenderer[] skinMeshList = GetComponentsInChildren<SkinnedMeshRenderer>();
        //foreach (SkinnedMeshRenderer smr in skinMeshList)
        //{
        //    material.....shader
        //}

        am.ac.camcon.lockTarget.GetComponentInChildren<Animator>().SetTrigger("hit");
        //DOTween...
        //am.ac.camcon.lockTarget.DOMove(am.ac.camcon.lockTarget.transform.position + transform.forward, 0.5f);
        am.ac.camcon.lockTarget.transform.position = Vector3.Lerp(am.ac.camcon.lockTarget.transform.position, am.ac.camcon.lockTarget.transform.position + transform.forward * 0.5f, 0.8f);

        StartCoroutine(PlayAnimation());
        //StartCoroutine(StopParticles());
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        anim.speed = 1;
    }

    IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(0.2f);
        //FX1.Stop();
        //FX2.Stop();
    }

    private void RevealModel(bool _state)
    {
        SkinnedMeshRenderer[] skinMeshList = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer smr in skinMeshList)
        {
            smr.enabled = _state;
        }
    }
}
