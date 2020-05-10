using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour
{
    public Button btnLogin;
    public Button btnLogin_NoServer;

    private void Start()
    {
        btnLogin.onClick.AddListener(() => LoginManager.instance.Login());
        btnLogin_NoServer.onClick.AddListener(() => LoginManager.instance.NoServerEnter());
    }
}
