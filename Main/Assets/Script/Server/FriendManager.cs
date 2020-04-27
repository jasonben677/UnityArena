using UnityEngine;

public class FriendManager : MonoBehaviour
{
    GameObject friend;
    Vector3 nextPos;
    Vector3 nextForward;
    Animator player2Anim;
    float runIndex;

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
        nextPos = new Vector3(_player.x, _player.y, _player.z);
        nextForward = (nextPos - friend.transform.position).normalized;
    }

    public void UpdateFriend()
    {
        float dis = (nextPos - friend.transform.position).magnitude;
        friend.transform.forward = nextForward;
        if (dis > 1.2f)
        {
            runIndex = 1;
            friend.transform.position = Vector3.Lerp(friend.transform.position, nextPos, Time.fixedDeltaTime);
        }
        else
        {
            runIndex = 0;
        }

        player2Anim.SetFloat("Blend", runIndex);
    }


}
