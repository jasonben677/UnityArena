using DG.Tweening;
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

    private GameObject playerHandle;

    private GameObject objFXA; //普攻A刀光特效
    private GameObject objFXB; //普攻B刀光特效
    private GameObject objFXC; //普攻C刀光特效
    private GameObject objFXD; //普攻C刀光特效
    private GameObject objFXE; //普功特效
    //private GameObject objFireRingFX;
    private GameObject objAttackFX;
    private Animator anim;
    //private float lerpWarp;

    [Space]
    [SerializeField]
    private GameObject slashFX;
    
    [Space]
    [SerializeField]
    private Material ghostMaterial;

    [Space]

    [Header("==== Particles ====")]
    [SerializeField]
    private ParticleSystem redTrail;
    [SerializeField]
    private ParticleSystem whiteTrail;

    [Space]

    [Header("==== Prefabs ====")]
    [SerializeField]
    private GameObject slashHitParticle;
    private GameObject prefabLastAttack;

    // Start is called before the first frame update
    void Start()
    {
        whR = transform.DeepFind("weaponHandleR").gameObject;
        //whL = transform.DeepFind("weaponHandleL").gameObject;
        
        weaponColR = whR.GetComponentInChildren<Collider>();
        //weaponColL = whL.GetComponentInChildren<Collider>();

        wcR = whR.GetComponent<WeaponController>();

        anim = transform.gameObject.GetComponent<Animator>();

        am = transform.gameObject.GetComponentInParent<ActorManager>();

        playerHandle = am.transform.gameObject;

        prefabLastAttack = Resources.Load("LastAttack") as GameObject;
    }

    private void FixedUpdate()
    {
        //lerpWarp += 0.3f * Time.fixedDeltaTime;

        if (am.ac.pi.isAI == false)
        {
            slashFX.transform.position = am.transform.position;
        }        
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

    public GameObject OnFXEnableD()
    {
        GameObject prefabFX = Resources.Load("FX004") as GameObject;
        objFXD = GameObject.Instantiate(prefabFX);
        objFXD.transform.parent = whR.transform;
        objFXD.transform.localPosition = Vector3.zero;
        objFXD.transform.localRotation = Quaternion.identity;

        return objFXD;
    }

    public void OnFXDisableD()
    {
        Destroy(objFXD.gameObject);
    }

    public GameObject OnFXEnableE()
    {
        //GameObject prefabFX = Resources.Load("LastAttack") as GameObject;
        objFXE = GameObject.Instantiate(prefabLastAttack);
        objFXE.transform.parent = whR.transform;
        objFXE.transform.localPosition = Vector3.zero;
        objFXE.transform.localRotation = Quaternion.identity;        

        return objFXE;
    }

    public void OnFXDisableE()
    {
        if (objFXE.gameObject != null)
        {
            Destroy(objFXE.gameObject);
        }
    }

    

    //public GameObject OnFireRingEnable()
    //{
    //    GameObject prefabFX = Resources.Load("FireRing") as GameObject;
    //    objFireRingFX = GameObject.Instantiate(prefabFX);
    //    objFireRingFX.transform.parent = playerHandle.transform;
    //    objFireRingFX.transform.localPosition = Vector3.zero;
    //    objFireRingFX.transform.localRotation = Quaternion.identity;

    //    return objFireRingFX;
    //}

    //public void OnFireRingDisable()
    //{
    //    Destroy(objFireRingFX.gameObject);
    //}

    public GameObject OnAttackFXEnable()
    {
        GameObject prefabFX = Resources.Load("AttackDauguan") as GameObject;
        objAttackFX = GameObject.Instantiate(prefabFX);
        objAttackFX.transform.parent = whR.transform;
        objAttackFX.transform.localPosition = Vector3.zero;
        objAttackFX.transform.localRotation = Quaternion.identity;

        return objAttackFX;
    }    

    /// <summary>
    /// slash技能
    /// </summary>
    public void OnWarp()
    {
        GameObject NinjiaClone = Instantiate(gameObject, transform.position, transform.rotation);
        Destroy(NinjiaClone.GetComponent<WeaponManager>().whR.gameObject);
        Destroy(NinjiaClone.GetComponent<Animator>());
        Destroy(NinjiaClone.GetComponent<WeaponManager>());


        SkinnedMeshRenderer[] skinMeshList = NinjiaClone.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer smr in skinMeshList)
        {
            //Debug.Log("materail changed");
            smr.material = ghostMaterial;
            //smr.material.DOFloat(4f, "_RimPower", 5f).OnComplete(() => Destroy(NinjiaClone));
            smr.material.DOFloat(5f, "_AllPower", 1f).OnComplete(() => Destroy(NinjiaClone));
        }

        RevealModel(false);
        anim.speed = 0;

        if (am.ac.camcon.lockTarget != null)
        {
            Debug.Log("OnWarpEnter");
            playerHandle.transform.DOMove(am.ac.camcon.lockTarget.transform.position, warpDuration).SetEase(Ease.InExpo).OnComplete(() => FinishWarp());
        }
        else
        {
            FinishWarp();
        }        
        //am.transform.position = Vector3.Lerp(am.transform.position, am.ac.camcon.lockTarget.transform.position, lerpWarp);
        //if (am.transform.position == am.ac.camcon.lockTarget.transform.position)
        //{
        //    Debug.Log("FinishWarp Enter");
        //    FinishWarp();
        //}   

        //Particles        
        redTrail.Play();
        whiteTrail.Play(); 
    }

    private void FinishWarp()
    {
        //Debug.Log("finishWarp Enter");
        RevealModel(true);

        Instantiate(slashHitParticle, whR.transform.position, Quaternion.identity);      

        am.ac.camcon.lockTarget.transform.position = Vector3.Lerp(am.ac.camcon.lockTarget.transform.position, am.ac.camcon.lockTarget.transform.position + transform.forward *0.95f, 0.8f);

        StartCoroutine(PlayAnimation());
        StartCoroutine(StopParticles());
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        anim.speed = 1;
    }

    IEnumerator StopParticles()
    {
        //Debug.Log("stopParticles");
        yield return new WaitForSeconds(0.2f);
        redTrail.Stop();
        whiteTrail.Stop();
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
