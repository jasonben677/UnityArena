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
    }

    // Update is called once per frame
    private void Update()
    {
        CameraRay();
    }

    void FixedUpdate()
    {        
        if(rayTerrain == true)
        {
            transform.position = rayHit.point;
            CameraMovement();
        }
        else
        {
            CameraMovement();
        }
                       
    }

    //private float offsetMagnitude;
    private bool CameraRay()
    {
        Vector3 handleToPos = transform.position - cameraHandle.transform.position;
                
        if(Physics.Raycast(cameraHandle.transform.position, handleToPos, out rayHit, 100.0f, LayerMask.GetMask("Terrain")))
        {
            Vector3 newPos = rayHit.point - cameraHandle.transform.position;
            if(newPos.magnitude < handleToPos.magnitude) //如果障礙物擋在中間
            {
                //this.transform.position = rayHit.point;
                //offsetMagnitude = newPos.magnitude;
                rayTerrain = true;
            }
        }
        else
        {
            rayTerrain =  false;
        }
        return rayTerrain;
    }

    private void CameraMovement()
    {         
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -20, 30); //限制camera上下的旋轉角度
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * verticalSpeed * Time.fixedDeltaTime);      
        
        model.transform.eulerAngles = tempModelEuler; //模型不會再跟著攝影機旋轉了~

        //讓camera追空物件，有延遲視覺效果
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 3.0f * Time.fixedDeltaTime);
        //camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampvelocity, cameraDampValue);
        //camera.transform.eulerAngles = transform.eulerAngles;
        camera.transform.LookAt(cameraHandle.transform);
        
    }
}
