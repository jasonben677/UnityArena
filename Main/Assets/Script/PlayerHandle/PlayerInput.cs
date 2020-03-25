using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    public bool inputEnable = true;  //若要暫時拿掉script，可改為false
    public bool run;

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
        targetDup = (Input.GetKey(KeyCode.W)? 1.0f : 0 )- (Input.GetKey(KeyCode.S)? 1.0f : 0);
        targetDright = (Input.GetKey(KeyCode.D) ? 1.0f : 0) - (Input.GetKey(KeyCode.A) ? 1.0f : 0);
       
        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        Dmag = Mathf.Sqrt((Dup * Dup) + (Dright * Dright)); //移動距離
        Dvec = Dup * transform.forward + Dright * transform.right; //移動方向

        run = Input.GetKey(KeyCode.LeftShift);

    }
}
