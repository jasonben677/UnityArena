using UnityEngine;

public class FriendManager : MonoBehaviour
{
    GameObject friend;
    ServerUserInput serverUser;
    Vector3 nextPos = Vector3.zero;
    Vector3 nextForward = Vector3.zero;
    Animator player2Anim;
    float runIndex = 0;
    bool attack = false;

    private void Awake()
    {
        friend = transform.GetChild(0).gameObject;
        serverUser = friend.GetComponent<ServerUserInput>();
        // 稍微暫改一下拿的位置
        player2Anim = friend.GetComponent<Animator>();
        //player2Anim = friend.GetComponentInChildren<Animator>();
        nextPos = friend.transform.position;

        //添加位移事件
        try
        {
            LoginManager.instance.client.tranmitter.Register(2, GetNextPos);
            LoginManager.instance.client.tranmitter.Register(3, GetAttackStatus);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.ToString());
        }

    }

    public void GetNextPos(Common.Tranmitter _tranmitter, TestDll.Message03 _player)
    {
        if (_player == null) return;
        Debug.Log("receive");

        nextPos = new Vector3(_player.friend.position[0], _player.friend.position[1], _player.friend.position[2]);
        nextForward = new Vector3(_player.friend.forward[0], _player.friend.forward[1], _player.friend.forward[2]);

        runIndex = _player.friend.moveStatus[0];
        attack = _player.friend.attackStatus;

        if (friend.activeSelf == false) 
        {
            friend.transform.position = nextPos;
            friend.transform.forward = nextForward;
        }

        friend.SetActive(true);
    }

    public void GetAttackStatus(Common.Tranmitter _tranmitter, TestDll.Message03 _player)
    {
        //player2Anim.SetTrigger("attack");
        serverUser.attack = _player.friend.attackStatus;
        Debug.Log("attack");
    }

    public void UpdateFriend()
    {
        if (!friend.activeSelf) 
        {
            return; 
        }


        if (nextForward != Vector3.zero)
        {
            serverUser.SetDir(nextForward);
        }

        friend.transform.position = Vector3.Lerp(friend.transform.position, nextPos, Time.fixedDeltaTime);

        serverUser.SetAnim(runIndex, attack, nextPos);
    }




}
