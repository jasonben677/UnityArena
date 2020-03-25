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
        Vector3 curxz = mainCam.transform.forward;
        curxz.y = 0;
        Vector3 move = (curxz * moveV) + (mainCam.transform.right * moveH);
        //move.y = _playerRigi.velocity.y;
        _playerRigi.position += move * _moveSpeed* run * Time.fixedDeltaTime;
        _player.transform.forward = move.normalized;
    }


    public void CameraMove(float _moveSpeed)
    {
        float moveH = (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0f) - (Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0f);
        float moveV = (Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0f) - (Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0f);
        myCam.transform.position = new Vector3(_player.transform.position.x, myCam.transform.position.y, _player.transform.position.z);

        if (moveH == 0 && moveV == 0)
        {
            return;
        }
        camRotate += new Vector3(moveV, moveH, 0);
        myCam.transform.rotation = Quaternion.Euler(camRotate);

        //mainCam.transform.position = myCam.transform.position + (-myCam.transform.forward);
        //Vector3 pos = new Vector3(_player.transform.position.x, myCam.transform.position.y, _player.transform.position.z);
        //myCam.transform.position = pos;
    }
}
