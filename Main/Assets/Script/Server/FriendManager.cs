using UnityEngine;
using TestDll;

public class FriendManager : MonoBehaviour
{
    GameObject friend;
    Vector3 nextPos;
    Animator player2Anim;
    float runIndex;
    private void Awake()
    {
        friend = transform.GetChild(0).gameObject;
        // 稍微暫改一下拿的位置
        player2Anim = friend.GetComponent<Animator>();
        //player2Anim = friend.GetComponentInChildren<Animator>();
        nextPos = friend.transform.position;
    }

    public void GetNextPos(Message _player)
    {
        if (_player == null) return;
        Debug.Log("receive");
        friend.SetActive(true);
        nextPos = new Vector3(_player.x, _player.y, _player.z);
        friend.transform.forward = (nextPos - friend.transform.position).normalized;
    }

    public void UpdateFriend()
    {
        float dis = (nextPos - friend.transform.position).magnitude;
        if (dis > 2.0f)
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
