using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigid : MonoBehaviour
{
    public Rigidbody Player;
    public float speed = 2.0f;
    public Animator Ani;
    string  sHorizontal = "Horizontal";
    string sVertical = "Vertical";
   
   

    // Start is called before the first frame update
    void Start()
    {
      
        this.Player = GetComponent<Rigidbody>();
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

        }
        else if (InputW | InputD | InputA | InputS)
        {
            PlayerAtcion(PLAYERACTION.Walk);
            speed = 2f;
            PlayerInstantSpeed();


        }
        else 
        {
            speed = 0;
            PlayerAtcion(PLAYERACTION.Idel); 
        }
    }
    



    private void PlayerAtcion(PLAYERACTION m_playeraction)
    {


        switch (_PlayerAtcion)
        {
            case PLAYERACTION.Idel:
                Ani.Play("Idel");
                break;
            case PLAYERACTION.Walk:
                Ani.Play("Walk");
                break;

            case PLAYERACTION.Run:
                Ani.Play("Run");
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
    private void PlayerInstantSpeed()
    {
        float InpVertical = Input.GetAxis(sVertical);
        float InpHorizontal = Input.GetAxis(sHorizontal);
        Vector3 direction = new Vector3(InpHorizontal, 0f, InpVertical);

        if (Input.anyKey)
        {

            Player.velocity = speed * direction;

        }
    }
    //0正前方90右方180後方270左方
    private void PlayerRotate()
    {
        Quaternion qRotat= this.transform.rotation;

        Debug.Log(qRotat);
    }


    private enum PLAYERACTION
    {
        Idel = 0,
        Walk,
        Run,
        Jump,
        Attack1,
        Attack2,
        Attack3
    }
    private PLAYERACTION _PlayerAtcion;
}
