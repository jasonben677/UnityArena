using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermobile : MonoBehaviour
{

    public Transform Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fX=Input.GetAxis("Horizontal");
        float fY=Input.GetAxis("Vertical");
        float fZ = Input.GetAxis("Jump");
        ThePlayermove(fX, fY, fZ);
        
    }


    private void ThePlayermove(float x,float y,float z) 
    {



        Vector3 vRight = Player.right;
        Vector3 vFor = Player.forward;

        Vector3 vm = x * vRight + y * vFor;
        Player.position = Player.position + vm * Time.deltaTime;

        //Jump未來新增暫時還想不到
        //Vector3 vJump = Player.;

    }
private void NpcRotation()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Player.position, Player.position + Player.forward * 2);
    }
}
