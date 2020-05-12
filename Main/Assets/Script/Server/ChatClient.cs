using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Common;
using TestDll;

public class ChatClient
{
    public Tranmitter tranmitter;
    public ChatClient()
    {
    }


    public bool Connect(string _address, int _port)
    {
        tranmitter = new Tranmitter(new TcpClient());

        if (tranmitter.Connect(_address, _port))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SendAccount(string _account, string _password)
    {
        tranmitter.mMessage.msgType = 0;
        tranmitter.mMessage.username = _account;
        tranmitter.mMessage.password = _password;
        tranmitter.Send();
    }

    public void SendPos(Vector3 _pos, Vector3 _forward, Vector2 _moveStatus)
    {
        tranmitter.mMessage.msgType = 2;
        tranmitter.mMessage.myPosition = new float[] { _pos.x, _pos.y, _pos.z };
        tranmitter.mMessage.myForward = new float[] { _forward.x, _forward.y, _forward.z };
        tranmitter.mMessage.myMoveStatus = new float[] { _moveStatus.x, _moveStatus.y };
        //Debug.Log("sendPos");
        tranmitter.Send();
    }

    public void Run()
    {
        tranmitter.Run();
    }

}
