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

    public void SetAnim(float _walk, bool _attack)
    {
        anim.SetFloat("forward", _walk);

        if (_attack)
        {
            anim.SetTrigger("attack");
        }
        
    }


}
