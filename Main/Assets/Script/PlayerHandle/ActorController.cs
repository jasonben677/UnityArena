using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 1.8f;
    public float runMultiplier = 3.0f;
    public float jumpVelocity = 3.5f; //向前跳的衝量使用
    public float rollVelocity = 2.0f; //向前翻滾的衝量使用

    [Space(10)]
    [Header("==== Friction Settings ====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec; //jump時候的衝量  
    private bool lockplanar; //鎖死平面移動(為了在jump的時候，不去更新planarVec)
    private bool canAttack;
    private CapsuleCollider col; //為了切換Physic material

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>(); //rigidbody!=null
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float targetRunMulti = ((pi.run) ? 2.0f : 1.0f); 
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.3f)); //調整走跑相互切換的流暢度

        if(rigid.velocity.magnitude > 0.5f)
        {
            anim.SetTrigger("roll");
        }

        if (pi.jump)//但這樣還是會有機會連跳，所以在animator的ground裡設程式碼調整
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }    
        
        if (pi.attack && CheckState("ground") && canAttack)
        {
            anim.SetTrigger("attack");
        }

        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f); //調整方向切換時的旋轉流暢度
            model.transform.forward = targetForward; 
        }

        if(lockplanar == false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
        }        
    }

    private void FixedUpdate()
    {
        //rigid.position += movingVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3 (planarVec.x, rigid.velocity.y, planarVec.z)+thrustVec;
        thrustVec = Vector3.zero;
    }

    private bool CheckState(string stateName, string LayerName = "Base Layer") 
    {
        int layerIndex = anim.GetLayerIndex(LayerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }

    /// 
    /// Message processing block
    /// 

    public void OnJumpEnter() //在animator的jump裡，設的條件
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnable = false;
        lockplanar = true;  //此時角色不會有平面方向的旋轉        
    }

    //public void OnJumpExist()
    //{
    //    pi.inputEnable = true;
    //    lockplanar = false;
    //}

    public void IsGround()
    {
        Debug.Log("is on ground");
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        Debug.Log("is not on ground");
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockplanar = false;
        canAttack = true;
        col.material = frictionOne;
    }

    public void OnGroundExist()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        pi.inputEnable = false;
        lockplanar = true;   
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnable = false;
        lockplanar = true;
    }

    public void OnJabEnter()
    {
        pi.inputEnable = false;
        lockplanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity"); //用動畫裡的curve調串接
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnable = false;
        //lockplanar = true;
        anim.SetLayerWeight( anim.GetLayerIndex("attack"), 1.0f);
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
    }

    public void OnAttackIdle()
    {
        pi.inputEnable = true;
        //lockplanar = false;
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0);
    }
}


