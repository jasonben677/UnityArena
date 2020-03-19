using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPlayerMovement : MonoBehaviour
{
    Rigidbody myRigi;
    Camera myCam;

    private void Awake()
    {
        myRigi = transform.GetComponent<Rigidbody>();
        myCam = Camera.main;
    }

    private void FixedUpdate()
    {
        float moveH = (Input.GetKey(KeyCode.D) ? 1.0f : 0f) - (Input.GetKey(KeyCode.A) ? 1.0f : 0f);
        float moveV = (Input.GetKey(KeyCode.W) ? 1.0f : 0f) - (Input.GetKey(KeyCode.S) ? 1.0f : 0f);
        if (moveH == 0 && moveV == 0)
        {
            return;
        }
        Vector3 move = (myCam.transform.forward * moveV) + (myCam.transform.right * moveH);
        move.y = 0;
        myRigi.velocity = move;
        transform.forward = move.normalized;
    }

}
