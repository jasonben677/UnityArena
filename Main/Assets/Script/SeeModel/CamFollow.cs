using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private float speed = 7.0f;
    float test = -90.0f;
    private Vector3 resetValue = new Vector3(95f, 4f, 92f);


    private void FixedUpdate()
    {
        float moveH = (Input.GetKey(KeyCode.D) ? 1.0f : 0f) - (Input.GetKey(KeyCode.A) ? 1.0f : 0f);
        float moveV = (Input.GetKey(KeyCode.W) ? 1.0f : 0f) - (Input.GetKey(KeyCode.S) ? 1.0f : 0f);
        float moveUp = (Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0f) - (Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0f);
        float rotate = (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0f) - (Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0f);


        if (moveH == 0 && moveV == 0 && moveUp == 0 && rotate == 0)
        {
            return;
        }

        Vector3 move = (transform.forward * moveV) + (transform.right * moveH) + (transform.up * moveUp);
        test += rotate *40.0f *Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, test, 0);
        transform.position += move * speed * Time.fixedDeltaTime;
    }

}
