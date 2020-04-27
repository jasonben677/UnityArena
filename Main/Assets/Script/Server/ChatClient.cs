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

    public void SendPos(float _x, float _y, float _z)
    {
        tranmitter.mMessage.msgType = 2;
        tranmitter.mMessage.x = _x;
        tranmitter.mMessage.y = _y;
        tranmitter.mMessage.z = _z;
        tranmitter.Send();
    }

    public void Run()
    {
        tranmitter.Run();
    }

}
