using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomManager : MonoBehaviour
{
    public GameObject playerName;

    private void Start()
    {
        GameObject obj = Instantiate(playerName,transform.GetChild(1));
        obj.transform.GetComponent<Text>().text = LoginManager.instance.client.tranmitter.mMessage.username;
        obj.SetActive(true);
        LoginManager.instance.client.tranmitter.mMessage.msgType = 1;
    }
}
