using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChat : MonoBehaviour
{
    public CharacterController Player;
    public float speed = 2.0f;
    public Animator Ani;
    string  sHorizontal = "Horizontal";
    string sVertical = "Vertical";
    float fSpeed = 0;

   

    // Start is called before the first frame update
    void Start()
    {
      
        this.Player = GetComponent<CharacterController>();
        this.Ani = this.GetComponent<Animator>();
        Ani.SetFloat("Walk", fSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        InputButton();
        PlayerRotate();
    }
    private void InputButton()
    {
        bool InputW = Input.GetKey(KeyCode.W);
        bool InputA = Input.GetKey(KeyCode.A);
        bool InputS = Input.GetKey(KeyCode.S);
        bool InputD = Input.GetKey(KeyCode.D);
        bool inputLShift = Input.GetKey(KeyCode.LeftShift);

        if (inputLShift & (InputW | InputD | InputA | InputS))
        {
            PlayerAtcion(PLAYERACTION.Run);
            speed = 4f;

            PlayerInstantSpeed();
            Debug.Log("Run");
        }
        else if (InputW | InputD | InputA | InputS)
        {
            PlayerAtcion(PLAYERACTION.Walk);
            speed = 2f;
            PlayerInstantSpeed();
            Debug.Log("walk");

            
        }
        else if (Input.GetKey(KeyCode.Space)) 
        {
            PlayerAtcion(PLAYERACTION.Jump);
            

        }
        else
        {
            speed = 0;
            PlayerAtcion(PLAYERACTION.Idle);
            
            Debug.Log("idle");
        }
    }
    



    private void PlayerAtcion(PLAYERACTION m_playeraction)
    {
        _PlayerAtcion = m_playeraction;

        switch (_PlayerAtcion)
        {
            case PLAYERACTION.Idle:
                if((fSpeed -= Time.deltaTime)<0)fSpeed=0;
                Ani.SetFloat("Walk", fSpeed);
                break;
            case PLAYERACTION.Walk:
                if((fSpeed+= Time.deltaTime)>0.3f)fSpeed=0.3f;
                Ani.SetFloat("Walk", fSpeed);

                break;

            case PLAYERACTION.Run:
                if((fSpeed+= Time.deltaTime)>1f)fSpeed=1f;
                Ani.SetFloat("Walk", fSpeed);
                break;

            case PLAYERACTION.Jump:
                break;

            case PLAYERACTION.Attack1:
                break;

            case PLAYERACTION.Attack2:
                break;

            case PLAYERACTION.Attack3:
                break;

        }
    }
    private void PlayerInstantSpeed()
    {
        float InpVertical = Input.GetAxis(sVertical);
        float InpHorizontal = Input.GetAxis(sHorizontal);


        Vector3 direction = new Vector3(InpHorizontal, 0f, InpVertical);
        direction = transform.TransformDirection(direction);


        direction *= speed;
  
        Player.Move(direction * Time.deltaTime);
        
    }

    //0正前方90右方180後方270左方
    private void PlayerRotate()
    {
        Quaternion PlayerRot=Player.transform.rotation;
        
    }


    private enum PLAYERACTION
    {
        Idle = 0,
        Walk,
        Run,
        Jump,
        Attack1,
        Attack2,
        Attack3
    }
    private PLAYERACTION _PlayerAtcion;
}
