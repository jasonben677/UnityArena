using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    public float cameraDampValue = 0.05f;
    //public float cameraDampRotation = 0.05f;    

    public Image lockDot;
    public GameObject playerHandle;
    private GameObject model;
    private GameObject cameraHandle;

    public GameObject lockTarget;
    public bool lockState;
    private Collider enemyCol;

    private Camera mainCamera;
    private float tempEulerX;
    private float tempEulerY;


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
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        cameraHandle.transform.position = playerHandle.transform.position + new Vector3(0, 2.0f, 0);
        tempEulerX = 20;
        model = playerHandle.GetComponent<ActorController>().model;

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

            if (Vector3.Distance(model.transform.position, lockTarget.transform.position) > 8.0f)
            {
                LockProcessA(null, false, false, pi.isAI);
            }
        }
        CameraRay();
    }

    void FixedUpdate()
    {
        //cameraLerpValue += 0.3f * Time.fixedDeltaTime;

        if (rayTerrain == true)
        {
            if (newOffset.magnitude < calRadius + 0.5f)
            {
                //Debug.Log("newoffset :" + newOffset.magnitude);                
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
        CameraRotate();
        CameraTranslate();
    }

    private void CameraRotate()
    {
        tempEulerY += pi.mouseRight * horizontalSpeed * Time.fixedDeltaTime;
        tempEulerX -= pi.mouseUp * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -30, 55);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, tempEulerY, 0);

        if (pi.isAI == false)
        {
            mainCamera.transform.position = transform.position;
            if (lockTarget == null)
            {
                mainCamera.transform.LookAt(cameraHandle.transform);
            }
            else
            {
                mainCamera.transform.LookAt(lockTarget.transform);
            }
        }

    }

    private void CameraTranslate()
    {
        if (pi.isAI == false)
        {
            cameraHandle.transform.position = Vector3.SmoothDamp(cameraHandle.transform.position, playerHandle.transform.position + new Vector3(0, 2.0f, 0), ref cameraDampvelocity, cameraDampValue);
        }
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
        Debug.Log("LockUnlock");
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
                    LockProcessA(lockTarget, true, true, pi.isAI);
                    break;
                }

            }
        }
    }

    //private class LockTarget
    //{
    //    public GameObject obj;
    //    public float halfHeight;

    //    public LockTarget(GameObject _obj, float _halfHeight)
    //    {
    //        obj = _obj;
    //        halfHeight = _halfHeight;
    //    }
    //}

}
