using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction 
{
   
    public  enum Anim
    {
    Idel,
    Walk,
    Run,
    Jump,
    Attack1,
    Attack2,
    Attack3
    }

    public bool InputPlayerWalk()
    {
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) |
              Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else
            return false;

    }
    public bool InputPlayrRun()
    {
        if ((Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.W)) |
            (Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.S)) |
            (Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.A)) |
            (Input.GetKey(KeyCode.LeftShift) & Input.GetKey(KeyCode.D)))
        {
            return true;
        }
        else
            return false;
    }
    //計算腳色移動速度
    







}
