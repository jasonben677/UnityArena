using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigid : MonoBehaviour
{
    public Rigidbody Player;
    public float speed = 2.0f;
    public Animator Ani; 
        // Start is called before the first frame update
    void Start()
    {
        this.Player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAtcion();     
            }

    private void PlayerInstantSpeed() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (Input.anyKey) 
         {

            Player.velocity = speed * direction;

        }
    }
    private void PlayerAtcion() 
    {

        if ((Input.GetKey(KeyCode.LeftShift)) & (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.D)
               | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S)))
        {
            _PlayerAction = PLAYERACTION.Run;
        }
        else if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.D)
               | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S))
        {
            _PlayerAction = PLAYERACTION.Walk;
        }
        else { _PlayerAction = PLAYERACTION.Idel; }

        switch (_PlayerAction) 
        {
            case PLAYERACTION.Idel:
                Ani.Play("Idel");
                speed = 0;
                PlayerInstantSpeed();
                break;
            case PLAYERACTION.Walk:
                Ani.Play("Walk");
                speed = 2f;
                PlayerInstantSpeed();
                break;
            
            case PLAYERACTION.Run:
                Ani.Play("Run");
                speed =4f;
                PlayerInstantSpeed();
                break;

            case PLAYERACTION.Jump:
                Ani.Play("Jump");
                break;

            case PLAYERACTION.Attack1:
                Ani.Play("Attack1");
                break;

            case PLAYERACTION.Attack2:
                Ani.Play("Attack2");
                break;

            case PLAYERACTION.Attack3:
                Ani.Play("Attack3");
                break;

        }
        
    }
    
   
    private enum PLAYERACTION 
    {Idel=0,
        Walk,
        Run,
        Jump,
        Attack1,
        Attack2,
        Attack3
    }
    private PLAYERACTION _PlayerAction;
}
