using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPlayerMovement
{
    Animator _playerAnim;
    Rigidbody _playerRigi;
    GameObject _player;
    GameObject myCam;
    Camera mainCam;
    Vector3 camRotate = new Vector3();
    float go;
    public float walkSign;


    public void SetPlayerComponent(Animator _animator, Rigidbody _rigidbody, GameObject _playerObj, GameObject _camControl)
    {
        _playerAnim = _animator;
        _playerRigi = _rigidbody;
        _player = _playerObj;
        myCam = _camControl;
        mainCam = Camera.main;
    }

    /// <summary>
    /// 腳色移動
    /// </summary>
    public void PlayerMove(float _moveSpeed)
    {
        float moveH = (Input.GetKey(KeyCode.D) ? 1.0f : 0f) - (Input.GetKey(KeyCode.A) ? 1.0f : 0f);
        float moveV = (Input.GetKey(KeyCode.W) ? 1.0f : 0f) - (Input.GetKey(KeyCode.S) ? 1.0f : 0f);
        float run = (Input.GetKey(KeyCode.LeftShift) ? 2.0f : 1.0f);
        Vector2 speed = new Vector2(moveH, moveV);
        walkSign = Mathf.SmoothDamp(walkSign, speed.magnitude * run, ref go, 0.3f);
        //float length = speed.magnitude;
        _playerAnim.SetFloat("Speed", walkSign);

        if (moveH == 0 && moveV == 0)
        {
            return;
        }
        Vector3 curxz = Quaternion.AngleAxis(-90, Vector3.up) * mainCam.transform.right;

        Vector3 move = (curxz * moveV) + (mainCam.transform.right * moveH);
        //move.y = _playerRigi.velocity.y;
        _playerRigi.position += move * _moveSpeed* run * Time.fixedDeltaTime;
        _player.transform.forward = move.normalized;
    }


    public void CameraMove(float _moveSpeed, float camHeight)
    {
        float moveH = (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0f) - (Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0f);
        float moveV = (Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0f) - (Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0f);

        RaycastHit hit;
        if (Physics.Linecast(mainCam.transform.position, myCam.transform.position, out hit, 1 << 9))
        {
            Debug.Log("aa");
            mainCam.transform.localPosition = new Vector3(0, 0, -1.0f);
        }
        else
        {
            if (!Physics.Raycast(mainCam.transform.position, -mainCam.transform.forward, out hit, 5.0f, 1 << 9))
            {
                Debug.Log("bb");
                mainCam.transform.localPosition = new Vector3(0, 0, -3.84f);
            }
            else
            {
                mainCam.transform.localPosition = new Vector3(0, 0, -1.0f);
            }
        }
        myCam.transform.position = _player.transform.position + new Vector3(0, camHeight, 0);

        myCam.transform.forward = (myCam.transform.position - mainCam.transform.position).normalized;
        if (moveH == 0 && moveV == 0)
        {
            return;
        }

        camRotate += new Vector3(moveV, moveH, 0);
        camRotate.x = Mathf.Clamp(camRotate.x, -40, 45);
        myCam.transform.rotation = Quaternion.Euler(camRotate);

        //mainCam.transform.position = myCam.transform.position + (-myCam.transform.forward);
        //Vector3 pos = new Vector3(_player.transform.position.x, myCam.transform.position.y, _player.transform.position.z);
        //myCam.transform.position = pos;
    }
}
