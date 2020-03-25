using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.8f;
    public float runMultiplier = 3.5f;
    public float jumpVelocity = 3.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;

    private bool lockplanar; //為了在jump的時候，不去更新planarVec

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>(); //rigidbody!=null
    }

    // Update is called once per frame
    void Update()
    {
        float targetRunMulti = ((pi.run) ? 2.0f : 1.0f); 
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.3f)); //調整走跑相互切換的流暢度
        if (pi.jump)
        {
            anim.SetTrigger("jump"); //但這樣還是會有機會連跳，所以在animator的ground裡設程式碼調整
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

    /// 
    /// Message processing block
    /// 

    public void OnJumpEnter() //在animator的jump裡，所設的方法
    {
        pi.inputEnable = false;
        lockplanar = true;  //此時角色不會有平面方向的旋轉
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    //public void OnJumpExist()
    //{
    //    pi.inputEnable = true;
    //    lockplanar = false;
    //}

    public void IsGround()
    {
        Debug.Log("is on ground");
        anim.SetBool("IsGround", true);
    }

    public void IsNotGround()
    {
        Debug.Log("is not on ground");
        anim.SetBool("IsGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockplanar = false;
    }
}


