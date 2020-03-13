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

        Vector3 vRight = Player.right;
        Vector3 vFor = Player.forward;
        Player.position = Player.position + (fX * vRight + fY * vFor) * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Player.position, Player.position + Player.forward * 2);
    }
}
