﻿using UnityEngine;
using TestDll;

public class FriendManager : MonoBehaviour
{
    GameObject friend;
    Vector3 nextPos;
    Animator player2Anim;
    float times = 0;
    private void Awake()
    {
        friend = transform.GetChild(0).gameObject;
        // 稍微暫改一下拿的位置
        //player2Anim = friend.GetComponent<Animator>();
        player2Anim = friend.GetComponentInChildren<Animator>();
        nextPos = friend.transform.position;
    }

    public void GetNextPos(Message _player)
    {
        if (_player == null) return;
        Debug.Log("receive");
        times = 0;
        friend.SetActive(true);
        nextPos = new Vector3(_player.x, _player.y, _player.z);
        friend.transform.forward = (nextPos - friend.transform.position).normalized;
    }

    public void UpdateFriend()
    {
        if (nextPos != friend.transform.position)
        {
            //times += Time.fixedDeltaTime;
            friend.transform.position = Vector3.Lerp(friend.transform.position, nextPos, 0.01f);
        }

        if (player2Anim.GetFloat("Blend") != 1)
        {
            player2Anim.SetFloat("Blend", 1);
        }

    }


}
