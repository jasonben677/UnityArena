using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermobile : MonoBehaviour
{
    public Animator PlayAni;
    public Transform Player;
    public float MoveSeed=2.0f;
    PlayerAction m_PlayerAction;
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
        NpcRotation(fX,fY);

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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10)) 
        {
            print(hit.point);
            print(hit.transform.position);
            print(hit.collider.gameObject);
            float PlayerHitDis = Vector3.Dot(Player.position, hit.transform.position);
        Debug.DrawLine(hit.transform.position,
            hit.transform.position+hit.transform.forward*3,Color.red);

        }

    }


    
    //腳色的轉向角度
private void NpcRotation(float Input_V ,  float Input_H)
    {
        float Sum_Angle = Mathf.Atan(Input_V/Input_H)/(Mathf.PI/180);


        Sum_Angle = Input_V < 0 ? Sum_Angle + 180 : Sum_Angle;



        if (float.IsNaN(Sum_Angle))
            Sum_Angle = 0;

        Debug.Log(Sum_Angle);
    
    
    
    
    
    
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Player.position, Player.position + Player.forward * 2);
    }
    private void OnMouseEnter()
    {
               
        Vector3 pos = Player.position;
       
        float fAng=Vector3.Angle(pos,Player.forward);
        
        Debug.Log(fAng);

    }

    
}
