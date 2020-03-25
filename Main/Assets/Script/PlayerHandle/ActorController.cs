using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.5f;
    public float runMultiplier = 3.5f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;

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
        //anim.SetFloat("forward", Mathf.Sqrt((pi.Dup * pi.Dup)+(pi.Dright * pi.Dright)));
        //model.transform.forward = pi.Dup * transform.forward + pi.Dright * transform.right;
        anim.SetFloat("forward", pi.Dmag * ((pi.run) ? 2.0f : 1.0f));
        if (pi.Dmag > 0.1f)
        {
            model.transform.forward = pi.Dvec;
        }
        movingVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
    }

    private void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
        //rigid.velocity = new Vector3 (movingVec.x, rigid.velocity.y, movingVec.z);
    }
}

