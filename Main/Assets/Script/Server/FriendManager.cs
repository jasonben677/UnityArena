using UnityEngine;
using TestDll;

public class FriendManager : MonoBehaviour
{
    GameObject friend;
    Vector3 nextPos;
    private void Awake()
    {
        friend = transform.GetChild(0).gameObject;
        nextPos = transform.position;
    }

    public void GetNextPos(Message _player)
    {
        if (_player == null) return;
        Debug.Log("receive");
        friend.SetActive(true);
        nextPos = new Vector3(_player.x, _player.y, _player.z);
    }

    public void UpdateFriend()
    {
        if (nextPos != friend.transform.position)
        {
            friend.transform.position = Vector3.Lerp(friend.transform.position, nextPos, 0.5f);
        }

    }


}
