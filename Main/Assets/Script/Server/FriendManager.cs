using UnityEngine;

public class FriendManager : MonoBehaviour
{
    GameObject friend;
    Vector3 nextPos = Vector3.zero;
    Vector3 nextForward = Vector3.zero;
    Animator player2Anim;
    float runIndex = 0;

    private void Awake()
    {
        friend = transform.GetChild(0).gameObject;
        // 稍微暫改一下拿的位置
        player2Anim = friend.GetComponent<Animator>();
        //player2Anim = friend.GetComponentInChildren<Animator>();
        nextPos = friend.transform.position;

        //添加位移事件
        try
        {
            LoginManager.instance.client.tranmitter.Register(2, GetNextPos);
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
        friend.SetActive(true);
        nextPos = new Vector3(_player.friend.position.x, _player.friend.position.y, _player.friend.position.z);
        nextForward = new Vector3(_player.friend.forward.x, _player.friend.forward.y, _player.friend.forward.z);
        runIndex = (_player.friend.moveStatus.forward > 0.05f) ? 1.0f : 0.0f;
    }

    public void UpdateFriend()
    {
        float dis = (nextPos - friend.transform.position).magnitude;

        if (nextForward != Vector3.zero)
        {
            friend.transform.forward = nextForward;
        }

        friend.transform.position = Vector3.Lerp(friend.transform.position, nextPos, Time.fixedDeltaTime * 2f);
        player2Anim.SetFloat("forward", runIndex);
    }


}
