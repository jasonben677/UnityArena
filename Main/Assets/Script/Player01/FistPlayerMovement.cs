using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPlayerMovement
{
    Animator _playerAnim;
    Rigidbody _playerRigi;
    GameObject _player;
    GameObject myCam;

    float go;
    public float walkSign;


    public void SetPlayerComponent(Animator _animator, Rigidbody _rigidbody, GameObject _playerObj, GameObject _camControl)
    {
        _playerAnim = _animator;
        _playerRigi = _rigidbody;
        _player = _playerObj;
        myCam = _camControl;
    }

    /// <summary>
    /// 腳色移動
    /// </summary>
    public void PlayerMove(float _moveSpeed)
    {
        float moveH = (Input.GetKey(KeyCode.D) ? 1.0f : 0f) - (Input.GetKey(KeyCode.A) ? 1.0f : 0f);
        float moveV = (Input.GetKey(KeyCode.W) ? 1.0f : 0f) - (Input.GetKey(KeyCode.S) ? 1.0f : 0f);

        Vector2 speed = new Vector2(moveH, moveV);
        walkSign = Mathf.SmoothDamp(walkSign, speed.magnitude, ref go, 0.3f);
        //float length = speed.magnitude;
        _playerAnim.SetFloat("Speed", walkSign);

        if (moveH == 0 && moveV == 0)
        {
            return;
        }

        Vector3 move = (myCam.transform.forward * moveV) + (myCam.transform.right * moveH);
        move.y = 0;
        _playerRigi.velocity = move * _moveSpeed;
        _player.transform.forward = move.normalized;
    }


    public void CameraMove(float _moveSpeed)
    {
        Vector3 pos = new Vector3(_player.transform.position.x, myCam.transform.position.y, _player.transform.position.z);
        myCam.transform.position = pos;
    }
}
