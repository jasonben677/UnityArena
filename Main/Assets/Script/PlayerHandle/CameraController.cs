using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public ActorManager am;
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    public float cameraDampValue = 0.15f;
    //public float cameraDampRotation = 0.05f;    

    public Image lockDot;
    public GameObject playerHandle;
    private GameObject model;
    private GameObject cameraHandle;

    public GameObject lockTarget;
    public bool lockState;
    private Collider enemyCol;
    private Collider cameraCol;
    private float cameraColRadius;

    private Camera mainCamera;
    private float tempEulerX;
    private float tempEulerY;


    //private float cameraLerpValue;
    private Vector3 cameraDampvelocity = Vector3.zero;
    private Vector3 currentPos; //用來存初設local位置
    private Vector3 tempPos;
    private float offset; //初設的距離
    
    private Vector3 dir;
    private float calRadius;
    private bool rayTerrain;
    private bool blockSight;
    RaycastHit rayHit;

    // Start is called before the first frame update
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        cameraHandle.transform.position = playerHandle.transform.position + new Vector3(0, 1.6f, 0);
        tempEulerX = 20;        
        model = playerHandle.GetComponent<ActorController>().model;
        cameraCol = cameraHandle.GetComponent<Collider>();
        cameraColRadius = 0.1f;

        if (pi.isAI == false)
        {
            mainCamera = Camera.main;
            lockDot.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            offset = (transform.position - cameraHandle.transform.position).magnitude;
            currentPos = transform.localPosition;
            calRadius = playerHandle.GetComponent<CapsuleCollider>().radius;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (lockTarget != null)
        {
            if (pi.isAI == false)
            {
                lockDot.rectTransform.position = mainCamera.WorldToScreenPoint(lockTarget.transform.position + new Vector3(0, enemyCol.bounds.extents.y, 0));
            }
            //Debug.Log(enemyCol.bounds.extents.y); //halfHeight

            if (Vector3.Distance(model.transform.position, lockTarget.transform.position) > 10.0f)
            {
                LockProcessA(null, false, false, pi.isAI);
            }

            if (am != null && am.sm.isDie)
            {
                LockProcessA(null, false, false, pi.isAI);
            }
        }        
    }

    void FixedUpdate()
    {
        //cameraLerpValue += 0.3f * Time.fixedDeltaTime;

        CameraRay();
        if (rayTerrain == true) //遮蔽物在射線範圍內
        {
            if (blockSight == true)
            {
                if (rayHit.distance < calRadius +0.35f) //如果太靠近角色模型
                {
                    //Debug.Log("rayHit.distance :" + rayHit.distance);                
                    //transform.RotateAround(cameraHandle.transform.position, cameraHandle.transform.right, 100f * Time.fixedDeltaTime);                    
                    tempPos = cameraHandle.transform.position + dir.normalized * (calRadius + 0.35f);
                    transform.position = tempPos;
                }
                else
                {
                    tempPos = rayHit.point - dir.normalized * 0.3f;
                    transform.position = tempPos;
                }                
            }
            else
            {
                transform.position = tempPos;               
            }            
        }
        else //遮蔽物不在射線範圍內
        {            
            transform.position = cameraHandle.transform.position + dir.normalized * offset;                       
            transform.localPosition = currentPos;
        }
        CameraRotate();
        CameraTranslate();
    }

    private void CameraRay() //障礙遮蔽判斷
    {
        dir = transform.position - cameraHandle.transform.position;

        //Physics.Raycast(cameraHandle.transform.position, dir, out rayHit, offset, LayerMask.GetMask("Terrain"));
        //Physics.BoxCast(cameraCol.bounds.center, cameraCol.bounds.extents, dir, out rayHit, cameraHandle.transform.localRotation, offset, LayerMask.GetMask("Terrain"));        

        bool rayResult = Physics.SphereCast(cameraCol.bounds.center, cameraColRadius, dir, out rayHit, offset, LayerMask.GetMask("Terrain"));

        if (rayResult == true)
        {
            //Debug.Log("rayResult: " + rayResult);
            if (rayHit.distance <= (transform.position - cameraHandle.transform.position).magnitude) //如果有障礙物夾在中間
            {
                rayTerrain = true;
                blockSight = true;
            }
            else
            {
                rayTerrain = true;
                blockSight = false;
            }
        }
        else
        {
            //Debug.Log("rayResult: " + rayResult);
            rayTerrain = false;
            blockSight = false;
        }        
    }

    private void CameraRotate()
    {
        tempEulerY += pi.mouseRight * horizontalSpeed * Time.fixedDeltaTime;
        tempEulerX -= pi.mouseUp * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -20, 55);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, tempEulerY, 0);

        if (pi.isAI == false)
        {
            mainCamera.transform.position = transform.position;
            //if (lockTarget == null)
            //{
                mainCamera.transform.LookAt(cameraHandle.transform);
            //}
            //else
            //{
            //    //mainCamera.transform.LookAt(lockTarget.transform);
            //    mainCamera.transform.LookAt(lockTarget.transform.position + new Vector3 (0, enemyCol.bounds.extents.y * 2.0f, 0));
            //}
        }

    }

    private void CameraTranslate()
    {
        if (pi.isAI == false)
        {
            //if ((playerHandle.transform.position - cameraHandle.transform.position).magnitude > 1.65f)
            //{
            //    cameraHandle.transform.position = Vector3.SmoothDamp(cameraHandle.transform.position, playerHandle.transform.position + new Vector3(0, 1.6f, 0), ref cameraDampvelocity, cameraDampValue);

            //}
            cameraHandle.transform.position = Vector3.SmoothDamp(cameraHandle.transform.position, playerHandle.transform.position + new Vector3(0, 1.6f, 0), ref cameraDampvelocity, cameraDampValue);
        }
    }    

    private void LockProcessA(GameObject _lockTarget, bool _lockDotEnable, bool _lockState, bool _isAI)
    {
        lockTarget = _lockTarget;
        if (pi.isAI == false)
        {
            lockDot.enabled = _lockDotEnable;
        }
        lockState = _lockState;
    }

    public void LockUnlock() //鎖/解鎖目標
    {
        //Debug.Log("LockUnlock");
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5.0f), model.transform.rotation, LayerMask.GetMask(pi.isAI ? "Player" : "Enemy"));

        if (cols.Length == 0)
        {
            LockProcessA(null, false, false, pi.isAI);
        }
        else
        {
            foreach (var col in cols)
            {
                //Debug.Log(col.name);
                if (lockTarget == col.gameObject)
                {
                    LockProcessA(null, false, false, pi.isAI);
                    break;
                }
                else
                {
                    lockTarget = col.gameObject;
                    enemyCol = col;
                    am = lockTarget.GetComponent<ActorManager>();
                    LockProcessA(lockTarget, true, true, pi.isAI);
                    break;
                }

            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    //Check if there has been a hit yet
    //    if (rayTerrain)
    //    {

    //        Gizmos.DrawRay(cameraHandle.transform.position, dir.normalized * rayHit.distance);

    //        Gizmos.DrawWireSphere(transform.position, cameraColRadius);
    //    }
    //    //If there hasn't been a hit yet, draw the ray at the maximum distance
    //    else
    //    {

    //        //Gizmos.DrawRay(cameraHandle.transform.position, dir.normalized * offset);

    //        Gizmos.DrawWireSphere(transform.position, cameraColRadius);
    //    }
    //}
}
