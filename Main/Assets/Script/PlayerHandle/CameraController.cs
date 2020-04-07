using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    //public float cameraDampValue = 0.01f;
        
    private GameObject playerHandle; //camera水平旋轉操控軸心
    private GameObject cameraHandle; //camera垂直旋轉操控軸心
    private float tempEulerX;
    private GameObject model;
    private Camera camera;

    //private Vector3 cameraDampvelocity;
    
    private Vector3 dir; //射線方向
    private float offset; //初設的距離
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
    }

    // Update is called once per frame
    private void Update()
    {
        CameraRay();
    }

    void FixedUpdate()
    {
        //if (rayTerrain == true)
        //{
        //    transform.position = rayHit.point;
        //    CameraMovement();
        //}
        //else //應該是這裡有問題，空物件(cameraPos)該怎麼回到初設的距離位置呢......
        //{            
        //    transform.position = cameraHandle.transform.position + dir.normalized * offset;
        //    CameraMovement();
        //}
        CameraMovement();
    }


    private bool CameraRay() //障礙物遮蔽判斷
    {
        dir = transform.position - cameraHandle.transform.position; 
        Physics.Raycast(cameraHandle.transform.position, dir, out rayHit, offset, LayerMask.GetMask("Terrain"));
        Vector3 newOffset = rayHit.point - cameraHandle.transform.position; //等等用來表示新的距離
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

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -20, 30); 
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);        
        
        model.transform.eulerAngles = tempModelEuler; //模型不會再跟著攝影機水平旋轉
        
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 2.6f * Time.fixedDeltaTime);
        //camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampvelocity, cameraDampValue);
        //camera.transform.eulerAngles = transform.eulerAngles;
        camera.transform.LookAt(cameraHandle.transform);
    }
}
