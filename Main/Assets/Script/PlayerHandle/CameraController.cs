﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    public float cameraDampValue = 0.01f;
        
    private GameObject playerHandle; //camera水平旋轉操控軸心
    private GameObject cameraHandle; //camera垂直旋轉操控軸心
    private float tempEulerX;
    private GameObject model;
    private Camera camera;

    private Vector3 cameraDampvelocity;
    private Vector3 currentPos; //用來存local位置    
    private float offset; //初設的距離
    private Vector3 newOffset;
    private Vector3 dir;
    private float calRadius;
    private bool rayTerrain;
    RaycastHit rayHit;

    // Start is called before the first frame update
    void Awake()
    {        
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main;       
        offset = (transform.position - cameraHandle.transform.position).magnitude;
        currentPos = transform.localPosition;
        calRadius = playerHandle.GetComponent<CapsuleCollider>().radius;        
    }

    // Update is called once per frame
    private void Update()
    {
        CameraRay();
    }

    void FixedUpdate()
    {
        if (rayTerrain == true)
        {
            if (newOffset.magnitude < calRadius + 0.5f)
            {
                //Debug.Log("newoffset :" + newOffset.magnitude);
                //playerHandle.transform.Rotate(playerHandle.transform.up, 200f * Time.fixedDeltaTime);
                transform.RotateAround(cameraHandle.transform.position, cameraHandle.transform.right, 100f * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = rayHit.point;
            }
        }
        else
        {            
            transform.localPosition = currentPos;            
        }
        CameraMovement();
    }


    private bool CameraRay() 
    {        
        dir = transform.position - cameraHandle.transform.position;
        
        Physics.Raycast(cameraHandle.transform.position, dir, out rayHit, offset, LayerMask.GetMask("Terrain"));
        newOffset = rayHit.point - cameraHandle.transform.position; 

        if (newOffset.magnitude < offset) //如果有障礙物夾在中間
        {
            rayTerrain = true;
        }
        else
        {
            rayTerrain = false;
        }
        return rayTerrain;
    }

    private void CameraMovement() //camera的旋轉控制，追隨空物件(cameraPos)設定
    {        
        Vector3 tempModelEuler = model.transform.eulerAngles;

        if (pi.Dright != 0)
        {
            playerHandle.transform.Rotate(Vector3.up, pi.Dright * 200f * Time.fixedDeltaTime);
        }
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -20, 70);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        //if (pi.Jright !=0)
        //{
        //    transform.RotateAround(cameraHandle.transform.position, cameraHandle.transform.up, pi.Jright * Time.fixedDeltaTime);
        //    camera.transform.position = transform.position;
        //}
        
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 3.0f * Time.fixedDeltaTime);

        //if (pi.Jright >= 0.5f || pi.Jright <= -0.5f)
        //{
        //    playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        //    camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 6.0f * Time.fixedDeltaTime);
        //}
        
        model.transform.eulerAngles = tempModelEuler; //模型不會再跟著攝影機水平旋轉       
        
        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 3.0f * Time.fixedDeltaTime);
        //camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampvelocity, cameraDampValue);
        //camera.transform.eulerAngles = transform.eulerAngles;
        camera.transform.LookAt(cameraHandle.transform);
    }
}
