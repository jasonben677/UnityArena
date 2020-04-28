using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("==== Output signals ====")]
    public float Dup;
    public float Dright;
    public float Dmag; //角色移動距離
    public Vector3 Dvec; //角色移動方向

    public float mouseUp; 
    public float mouseRight;

    public bool lockon;
    public bool run;
    public bool defense;
    public bool counterBack;
    public bool jump;
    private bool lastJump;
    public bool attack;
    private bool lastAttack;

    [Header("==== others ====")]
    public bool inputEnable = true;
    public bool isAI = false;
    public bool trackDirection = false;
    public CameraController camcon;
        
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    
    private Vector3 cameraForward;
    private Vector3 cameraRight;    
    private Vector3 tempCameraForward;
    private Vector3 tempCameraRight;    
    private GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        model = transform.gameObject.GetComponent<ActorController>().model;
    }

    // Update is called once per frame
    void Update()
    {
        if (camcon.lockTarget == null)
        {
            cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
        }
        else
        {
            if(isAI == false)
            {
                tempCameraForward = model.transform.forward;
                tempCameraForward.y = 0;
                tempCameraRight = model.transform.right;
                tempCameraRight.y = 0;
            }            
        }

        targetDup = Input.GetAxis("Vertical");
        targetDright = Input.GetAxis("Horizontal");

        mouseUp = Input.GetAxis("Mouse Y");
        mouseRight = Input.GetAxis("Mouse X");

        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.05f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.05f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));

        if (camcon.lockState == false)
        {
            Dvec = Dup2 * cameraForward + Dright2 * cameraRight;
        }
        else
        {
            if(trackDirection == false)
            {
                Dvec = Dup2 * tempCameraForward + Dright2 * tempCameraRight;
            }
            else
            {
                Dvec = Dup2 * cameraForward + Dright2 * cameraRight;
            }
        }

        lockon = Input.GetMouseButtonDown(1); //鎖定目標

        run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        defense = Input.GetKey(KeyCode.F);
        counterBack = Input.GetKey(KeyCode.R);

        bool newJump = Input.GetKey(KeyCode.Space); //跳躍觸發設定       
        if(newJump != lastJump && newJump == true)
        {
            jump = true;
            //Debug.Log("jump!!");
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        bool newAttack = Input.GetMouseButton(0); //單手普攻觸發設定
        if (newAttack != lastAttack && newAttack == true)
        {
            attack = true;
            //Debug.Log("attack!!");
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
    }

    private Vector2 SquareToCircle(Vector2 input) //使前後和斜向的移動距離一樣: Elliptical grid mapping
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f); //套Elliptical grid mapping的求距離公式
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    public void UpdateDmagDvec(float Dup, float Dright)
    {
        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);
        Dvec = Dright * cameraRight + Dup * cameraForward;
    }
}
