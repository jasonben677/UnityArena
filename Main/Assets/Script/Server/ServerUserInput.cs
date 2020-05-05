using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerUserInput : PlayerInput
{
    Animator anim;


    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDir(Vector3 _dir)
    {
        transform.forward = _dir;
    }

    public void SetAnim(float _walk, bool _attack, Vector3 _pos)
    {
        if (anim == null) { return; }

        //float curWalk = Mathf.Lerp(anim.GetFloat("forward"), _walk, 0.75f);

        float dis = (_pos - transform.position).magnitude;

        //_walk = (_walk > 0.05f) ? 1.0f : 0f;

        if (dis < 0.5f)
        {
            anim.SetFloat("forward", 0);
        }
        else
        {
            anim.SetFloat("forward", 1);
        }


        if (_attack)
        {
            anim.SetTrigger("attack");
        }
        
    }


}
