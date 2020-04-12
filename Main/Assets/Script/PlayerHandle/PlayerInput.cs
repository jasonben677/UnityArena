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

    public float mouseUp; //camera的旋轉控制
    public float mouseRight;

    public bool run;
    public bool jump;
    private bool lastJump;
    public bool attack;
    private bool lastAttack;

    [Header("==== others ====")]

    public bool inputEnable = true;
        
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cameraForward = Camera.main.transform.forward;  
        cameraRight = Camera.main.transform.right;      

        targetDup = Input.GetAxis("Vertical");
        targetDright = Input.GetAxis("Horizontal");

        mouseUp = Input.GetAxis("Mouse Y");
        mouseRight = Input.GetAxis("Mouse X");

        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.01f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.01f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2)); 
        Dvec = Dup2 * new Vector3 (cameraForward.x, 0, cameraForward.z) + Dright2 * new Vector3(cameraRight.x, 0, cameraRight.z); 
                
        run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        bool newJump = Input.GetKey(KeyCode.Space); //跳躍觸發設定       
        if(newJump != lastJump && newJump == true)
        {
            jump = true;
            Debug.Log("jump!!");
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
            Debug.Log("attack!!");
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
}
