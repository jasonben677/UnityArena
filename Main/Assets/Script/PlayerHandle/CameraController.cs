using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    public float cameraDampValue = 0.05f;
    //public float cameraDampRotation = 0.05f;

    public GameObject playerHandle; 

    private GameObject cameraHandle;

    
    private float tempEulerX;
    private float tempEulerY;
    //private GameObject model;
    private Camera mainCamera;

    //private float cameraLerpValue;
    private Vector3 cameraDampvelocity = Vector3.zero;
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
        cameraHandle.transform.position = playerHandle.transform.position + new Vector3(0, 2.0f, 0);  
        tempEulerX = 20;
        //model = playerHandle.GetComponent<ActorController>().model;
        mainCamera = Camera.main;       
        offset = (transform.position - cameraHandle.transform.position).magnitude;
        currentPos = transform.localPosition;
        calRadius = playerHandle.GetComponent<CapsuleCollider>().radius;

        Cursor.lockState = CursorLockMode.Locked;        
    }

    // Update is called once per frame
    private void Update()
    {
        
        CameraRay();
    }

    void FixedUpdate()
    {        
        //cameraLerpValue += 0.3f * Time.fixedDeltaTime;

        if (rayTerrain == true)
        {
            if (newOffset.magnitude < calRadius + 0.5f) // 若碰撞點過近，camera看到的角色有可能會破面
            {
                //Debug.Log("newoffset :" + newOffset.magnitude);
                //將空物件(cameraPos，維持offset，向上抬升)
                transform.RotateAround(cameraHandle.transform.position, cameraHandle.transform.right, 100f * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = rayHit.point; //但還是會有牆壁破面的小問題，因為剛好座標點在牆上(不要刻意用滑鼠去撞牆可暫時避免)
            }
        }
        else
        {            
            transform.localPosition = currentPos;            
        }
        CameraRotate();
        CameraTranslate();
    }

    private void CameraRotate() //follow 空物件(cameraPos)
    {   
        tempEulerY += pi.mouseRight * horizontalSpeed * Time.fixedDeltaTime;
        tempEulerX -= pi.mouseUp * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -30, 60);

        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, tempEulerY, 0);

        
        mainCamera.transform.position = transform.position;
        mainCamera.transform.LookAt(cameraHandle.transform);
    }

    private void CameraTranslate()
    {        
        cameraHandle.transform.position = Vector3.SmoothDamp(cameraHandle.transform.position, playerHandle.transform.position + new Vector3(0, 2.0f, 0), ref cameraDampvelocity, cameraDampValue);
    }


    private bool CameraRay() //障礙遮蔽判斷
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

    

    //private void CameraMoveTranslation() 
    //{        

    //    //playerHandle.transform.Rotate(Vector3.up, pi.Dright * 200f * Time.fixedDeltaTime); //為避免轉角處遮蔽，使角色 A、D鍵移動時，呈圓周軌跡運動             

    //    //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, transform.position, cameraLerpValue * Time.fixedDeltaTime);
    //    mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref cameraDampvelocity, cameraDampValue);

    //    mainCamera.transform.LookAt(cameraHandle.transform);
    //}

    //private void CameraMoveRotation()
    //{
    //    Vector3 tempModelEuler = model.transform.eulerAngles; //存角色當下的角度

    //    playerHandle.transform.Rotate(Vector3.up, pi.mouseRight * horizontalSpeed * Time.fixedDeltaTime); //以playerHandle當軸心，cameraPos可水平環繞角色

    //    model.transform.eulerAngles = tempModelEuler; //角色不會再跟著攝影機一起做水平旋轉

    //    tempEulerX -= pi.mouseUp * verticalSpeed * Time.fixedDeltaTime;
    //    tempEulerX = Mathf.Clamp(tempEulerX, -20, 70);
    //    cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0); //以cameraHandle當軸心，cameraPos可俯仰視角色

    //    mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref cameraDampvelocity, cameraDampRotation);
    //    mainCamera.transform.LookAt(cameraHandle.transform);
    //}



}
