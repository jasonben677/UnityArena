using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermobile : MonoBehaviour
{
    public Animator PlayAni;
    public Transform Player;
    public float MoveSeed=2.0f;
    PlayerAction m_PlayerAction;
    public Transform Target;
    private void Awake()
    {
    }
    void Start()
    {
        m_PlayerAction = new PlayerAction();

    }

    // Update is called once per frame
    void Update()
    {
        float fY = Input.GetAxis("Vertical");
        float fX = Input.GetAxis("Horizontal");


        
        if (m_PlayerAction.InputPlayrRun())
        {
            MoveSeed = 4.0f;
            PlayAni.Play("Run");
           ThePlayermove(fX , fY , 0);

        }
        else if (m_PlayerAction.InputPlayerWalk())
        {
            MoveSeed = 2.0f;
            PlayAni.Play("Walk");
            ThePlayermove(fX, fY, 0);
          

        }
        else { PlayAni.Play("Idel"); }
     
        NpcRotation();
    }

    public void ThePlayermove(float x, float y, float z)
    {



        Vector3 vRight = Player.right;
        Vector3 vFor = Player.forward;

        Vector3 vm = x * vRight + y * vFor;
        Player.position = Player.position + vm * MoveSeed * Time.deltaTime;

        //Jump Z軸的位移，加上重力
        //Vector3 vJump = Player.;

    }





    //判斷角色Ray是否碰牆,如果有moveseed=0
    public void  PlayerRay()
    {
       bool n=Physics.Raycast(Player.position, transform.forward, 10);
        Debug.DrawRay(Player.position, transform.forward ,Color.green);

    }


    private void PlayerRotate ()
    {
    //if (Input.GetKey(KeyCode.W)){transform.Rotate ;}
    //    if (Input.GetKey(KeyCode.S)){ transform.Rotate ;}
    //    if (Input.GetKey(KeyCode.A)){ transform.Rotate = Player.position + new Vector3(-1, 0, 0);}
    //    if (Input.GetKey(KeyCode.D)){ transform.Rotate = Player.position + new Vector3(1, 0, 0);}
    //    if (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.A)) { transform.Rotate = Player.position + new Vector3(-1, 0, 1); }
    //    if (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.D)) { transform.Rotate = Player.position + new Vector3(1, 0, 1); }
    //    if (Input.GetKey(KeyCode.A) & Input.GetKey(KeyCode.S)) { transform.Rotate = Player.position + new Vector3(-1, 0, -1); }
    //    if (Input.GetKey(KeyCode.S)&Input.GetKey(KeyCode.D)){ transform.Rotate = Player.position + new Vector3(1, 0, -1); }
   
    }
    
    //腳色的轉向角度
private void NpcRotation()
    {
        
        
        
        
        
        
        
        
        
        
        
        Vector3 vPlayerNormaliz = (Target.transform.position - Player.position).normalized;
        float PlayerDot = Vector3.Dot(transform.forward, vPlayerNormaliz);





        //float Sum_Angle = Mathf.Atan(Input_V / Input_H) / (Mathf.PI / 180);


        //Sum_Angle = Input_V < 0 ? Sum_Angle + 180 : Sum_Angle;



        //if (float.IsNaN(Sum_Angle))
        //    Sum_Angle = 0;

        Debug.Log(PlayerDot);

        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawLine(Player.position, Player.position + Player.forward * 2);
    }
    /*
    private void NPCAnimation()
    {


        switch () 
        {
            case PlayerAction.Anim.Walk:
                PlayAni.Play("Walk");
                break;
            case PlayerAction.Anim.Run:
                PlayAni.Play("Run");

                break;
            case PlayerAction.Anim.Jump:
                PlayAni.Play("Jump");

                break;
            case PlayerAction.Anim.Attack1:
                PlayAni.Play("Attack1");
                break;
            case PlayerAction.Anim.Attack2:
                PlayAni.Play("Attack2");
                break;
            case PlayerAction.Anim.Attack3:
                PlayAni.Play("Attack3");
                break;
        }
    }*/

    //private void OnMouseEnter()
    //{

    //    Vector3 pos = Player.position;

    //    float fAng=Vector3.Angle(pos,Player.forward);

    //    Debug.Log(fAng);

    //}


}
