using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermobile : MonoBehaviour
{
    public Animator PlayAni;
    public Transform Player;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fY = Input.GetAxis("Vertical");
        float fX = Input.GetAxis("Horizontal");

        if (InputPlayrRun())
        {
            PlayAni.Play("Run");
            ThePlayermove(fX * 2, fY * 2, 0);

        }
        else if (InputPlayerWalk())
        {
            PlayAni.Play("Walk");
            ThePlayermove(fX, fY, 0);
        }
        else { PlayAni.Play("Idel"); }

    }

    //判斷是否按下走路的移動
    private bool InputPlayerWalk()
    {
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) |
              Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else       
        return false;
        
    }
    private bool InputPlayrRun() 
    {
        if ((Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.W)) |
            (Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.S)) | 
            (Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.A)) | 
            (Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.D)))
        { 
            return true;
        }
        else 
            return false;
    }

    //計算腳色移動速度
    private void ThePlayermove(float x,float y,float z) 
    {



        Vector3 vRight = Player.right;
        Vector3 vFor = Player.forward;

        Vector3 vm = x * vRight + y * vFor;
        Player.position = Player.position + vm *2.0f* Time.deltaTime;

        //Jump未來新增暫時還想不到
        //Vector3 vJump = Player.;

    }
    //腳色的轉向角度
private void NpcRotation()
    {
        Vector3 pos = Player.position;
        Vector3.Dot(Player.position,pos);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Player.position, Player.position + Player.forward * 2);
    }
    private void OnMouseEnter()
    {
        
    
    
        float fY = Input.GetAxis("Vertical");
        float fX = Input.GetAxis("Horizontal");
        Vector3 vRight = Player.right;
        Vector3 vFor = Player.forward;

        Vector3 vm = fX * vRight + fY * vFor;
        Vector3 pos = Player.position;
        Vector3 nextPlayerpos = Player.position + vm * 2.0f;
        float pDot = Vector3.Dot(Player.position, nextPlayerpos);
        Debug.Log(pDot);
    }
}
