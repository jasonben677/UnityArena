using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public CameraController camcon;
    public PlayerInput pi;
    public float walkSpeed = 5.5f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 2.0f; //向前跳的衝量使用
    public float rollVelocity = 1.5f; //向前翻滾的衝量使用

    // server
    private bool useServer = false;
    [SerializeField] FriendManager friend;
    // 發送時間
    private float sendTime = 0.0f;

    [Space(10)]
    [Header("==== Friction Settings ====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec; //jump時候的衝量  
    private bool lockplanar = false; //鎖死平面移動(為了在jump的時候，不去更新planarVec)
    private bool canAttack;
    //public bool trackDirection = false;
    private CapsuleCollider col; //為了切換Physic material
    //private float lerpTarget;
    private Vector3 deltaPos;
    private Vector3 localDvec;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>(); //rigidbody!=null
        col = GetComponent<CapsuleCollider>();

        //判斷server
        try
        {
            if (LoginManager.instance.client.tranmitter != null && gameObject.layer == 11)
            {
                useServer = true;
            }
            else
            {
                useServer = false;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.ToString());
            useServer = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        localDvec = model.transform.InverseTransformVector(pi.Dvec);

        if (pi.lockon)
        {
            camcon.LockUnlock();
        }

        if (camcon.lockState == false)
        {
            float targetRunMulti = ((pi.run) ? 2.0f : 1.0f);
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.3f)); //調整走跑相互切換的流暢度
            anim.SetFloat("right", 0);
        }
        else
        {

            anim.SetFloat("forward", localDvec.z * ((pi.run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDvec.x * ((pi.run) ? 2.0f : 1.0f));
        }

        if (pi.jump || rigid.velocity.magnitude > 7.0f)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (pi.jump)//但這樣還是會有機會連跳，所以在animator的ground裡設程式碼調整
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if ((CheckState("ground") || CheckStateTag("attackR")) && canAttack)
        {
            if (pi.attack) //普通攻擊
            {
                if (gameObject.layer == 11 && useServer)
                {
                    Debug.Log("CAttack");
                    LoginManager.instance.SetAttack(true);
                }
                anim.SetTrigger("attack");
            }

            if (pi.counterBack) //反擊
            {
                anim.SetTrigger("counterBack");
            }
        }        

        if (pi.defense)
        {
            if(CheckState("ground") || CheckState("blocked"))
            {
                anim.SetBool("defense", pi.defense);
                anim.SetLayerWeight ( anim.GetLayerIndex("defense"), 1);
            }
            else
            {
                anim.SetBool("defense", false);
                anim.SetLayerWeight ( anim.GetLayerIndex("defense"), 0);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);

        }


        if (camcon.lockState == false) //判斷有無鎖目標(會影響移動時的面部朝向)
        {
            if (pi.Dmag > 0.1f)
            {
                Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.25f); //調整方向切換時的旋轉流暢度
                model.transform.forward = targetForward;
            }

            if (lockplanar == false)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
        else
        {
            if (pi.trackDirection == false)
            {
                if (pi.isAI == false) {
                    Vector3 tempDvec = camcon.lockTarget.transform.position - model.transform.position;
                    tempDvec.y = 0;
                    model.transform.forward = tempDvec;
                }
                else
                {
                    Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.25f); //調整方向切換時的旋轉流暢度
                    model.transform.forward = targetForward;
                }
                

                if (lockplanar == false)
                {
                    planarVec = pi.Dmag * pi.Dvec * walkSpeed * 0.8f * ((pi.run) ? runMultiplier : 1.0f);
                }
            }
            else
            {
                model.transform.forward = planarVec.normalized;

                if (lockplanar == false)
                {
                    planarVec = pi.Dmag * pi.Dvec * walkSpeed * 0.8f * ((pi.run) ? runMultiplier : 1.0f);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        //rigid.position += movingVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;

        //發送角色位置
        sendTime += Time.fixedDeltaTime;
        if (sendTime >= 0.25f && useServer)
        {
            //bool playerAttack = false;

            //if (CheckState("attack1hA") || CheckState("attack1hB") || CheckState("attack1hC"))
            //{
            //    playerAttack = true;
            //}
            //else
            //{
            //    playerAttack = false;
            //}

            Vector2 moveStatus = new Vector2(anim.GetFloat("forward"), anim.GetFloat("right"));
            //LoginManager.instance.SetAttack(playerAttack);
            LoginManager.instance.SendPos(transform.position, model.transform.forward, moveStatus);
            sendTime = 0;
        }

        friend?.UpdateFriend();
    }

    public bool CheckState(string stateName, string LayerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(LayerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }

    public bool CheckStateTag(string tagName, string LayerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(LayerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
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
        pi.trackDirection = true;
    }

    //public void OnJumpExist()
    //{
    //    pi.inputEnable = true;
    //    lockplanar = false;
    //}

    public void IsGround()
    {
        //Debug.Log("is on ground");
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        //Debug.Log("is not on ground");
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockplanar = false;
        canAttack = true;
        col.material = frictionOne;
        pi.trackDirection = false;
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
        //thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnable = false;
        lockplanar = true;
        pi.trackDirection = true;
    }

    public void OnRollUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("rollSpeed");
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
        //lerpTarget = 1.0f;

    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        // float currentWeight = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.1f);
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.8f)); //使切換攻擊圖層較平緩
    }

    public void OnAttackExist()
    {
        model.SendMessage("WeaponDisable");
    }

    public void OnHitEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnDieEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnBlockedEnter()
    {
        pi.inputEnable = false;
    }

    public void OnStunnedEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnCounterBackEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC"))
        {
            deltaPos += (Vector3)_deltaPos;
        }
        deltaPos += (0.2f * deltaPos + 0.8f * (Vector3)_deltaPos) / 1.0f;
    }

    public void IssueTrigger( string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
}


