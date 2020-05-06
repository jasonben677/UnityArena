using UnityEngine;

public class FriendManager : MonoBehaviour
{
    int maxFriend = 2;

    GameObject[] friend;
    ServerUserInput[] serverUser;

    TestDll.Message03 curMessage;

    private void Awake()
    {
        friend = new GameObject[maxFriend];
        serverUser = new ServerUserInput[maxFriend];

        friend[0] = transform.GetChild(0).gameObject;
        friend[1] = transform.GetChild(1).gameObject;

        serverUser[0] = friend[0].GetComponent<ServerUserInput>();
        serverUser[1] = friend[1].GetComponent<ServerUserInput>();

        //添加位移事件
        try
        {
            LoginManager.instance.client.tranmitter.Register(1, FriendLogout);
            LoginManager.instance.client.tranmitter.Register(2, GetNextPos);
            LoginManager.instance.client.tranmitter.Register(3, GetAttackStatus);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.ToString());
        }

    }

    public void FriendLogout(Common.Tranmitter _tranmitter, TestDll.Message03 _player)
    {
        Debug.Log("Friend logout");
        _tranmitter.mMessage = _player;
        curMessage = _tranmitter.mMessage;
    }

    public void GetNextPos(Common.Tranmitter _tranmitter, TestDll.Message03 _player)
    {
        Debug.Log("receive");
        _tranmitter.mMessage = _player;
        curMessage = _tranmitter.mMessage;
    }

    public void GetAttackStatus(Common.Tranmitter _tranmitter, TestDll.Message03 _player)
    {
        _tranmitter.mMessage = _player;
        curMessage = _tranmitter.mMessage;
        Debug.Log("attack");
    }

    public void UpdateFriend()
    {
        if (curMessage == null)
        {
            return;
        }
        for (int i = 0; i < curMessage.friend.Length; i++)
        {
            TestDll.Player tempFriend = curMessage.friend[i];

            if (tempFriend == null)
            {
                friend[i].SetActive(false);
            }
            else
            {
                if (tempFriend.name != null)
                {
                    serverUser[i].SetDir(new Vector3(tempFriend.forward[0], tempFriend.forward[1], tempFriend.forward[2]));

                    if (friend[i].activeSelf == false)
                    {
                        friend[i].transform.position = new Vector3(tempFriend.position[0], tempFriend.position[1], tempFriend.position[2]);
                    }
                    else
                    {
                        friend[i].transform.position = Vector3.Lerp(friend[i].transform.position,
                            new Vector3(tempFriend.position[0], tempFriend.position[1], tempFriend.position[2]), Time.fixedDeltaTime);
                    }

                    serverUser[i].SetAnim(tempFriend.moveStatus[0], tempFriend.attackStatus,
                        new Vector3(tempFriend.position[0], tempFriend.position[1], tempFriend.position[2]));

                    serverUser[i].UpdatePlayerState(tempFriend.hp, tempFriend.atkDamage);
                    friend[i].SetActive(true);
                }
            }

        }
    }

}
