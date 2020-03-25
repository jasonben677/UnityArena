using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Dup;
    public float Dright;
    public float Dmag; //角色移動距離
    public Vector3 Dvec; //角色移動方向

    public float JUp; //camera的旋轉控制
    public float JRight;

    public bool inputEnable = true; 
    public bool run; //pressing signal

    public bool jump; //trigger once signal
    private bool lastJump;
    

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetDup = (Input.GetKey(KeyCode.W)? 1.0f : 0)- (Input.GetKey(KeyCode.S)? 1.0f : 0);
        targetDright = (Input.GetKey(KeyCode.D) ? 1.0f : 0) - (Input.GetKey(KeyCode.A) ? 1.0f : 0);

        JUp = (Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0);
        JRight = (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0);

        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2)); 
        Dvec = Dup2 * transform.forward + Dright2 * transform.right; 

        run = Input.GetKey(KeyCode.LeftShift);

        bool newJump = Input.GetKey(KeyCode.J);        
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
    }

    private Vector2 SquareToCircle(Vector2 input) //使前後和斜向的移動距離一樣: Elliptical grid mapping
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }
}
