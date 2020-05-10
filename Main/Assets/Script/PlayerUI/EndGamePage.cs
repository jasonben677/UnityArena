using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerLeft;
    [SerializeField] TextMeshProUGUI titleInfo;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        LoginManager.instance.ScenceFadeIn();
        int _playerLeft = LoginManager.instance.client.tranmitter.mMessage.playerLeft;
        playerLeft.text = _playerLeft.ToString();

        if (_playerLeft <= 1)
        {
            titleInfo.text = "恭喜 你是贏家";
        }
        else
        {
            titleInfo.text = "";
        }

        LoginManager.instance.GoToEndGamePanel();
    }


    public void BackToLogin()
    {
        LoginManager.instance.ScenceFadeOut();
    }

}
