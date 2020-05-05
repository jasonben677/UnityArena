using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TestDll;

public class LoginManager : MonoBehaviour
{
    static public LoginManager instance;
    public ChatClient client = null;
    bool connectSucceed = false;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        Button button = GameObject.Find("LoginButton").GetComponent<Button>();
        button.onClick.AddListener(() => Login());
    }

    private void FixedUpdate()
    {
        if (connectSucceed)
        {
            client.Run();
        }
    }


    private void OnDisable()
    {
        if (client != null)
        {
            client.tranmitter.Close();
        }

    }

    public void SendPos(Vector3 pos, Vector3 forward, Vector2 moveStatus)
    {
        float newPosX = (float)System.Math.Round(pos.x, 2);
        float newPosY = (float)System.Math.Round(pos.y, 2);
        float newPosZ = (float)System.Math.Round(pos.z, 2);

        float newforwardX = (float)System.Math.Round(forward.x, 2);
        float newforwardY = (float)System.Math.Round(forward.y, 2);
        float newforwardZ = (float)System.Math.Round(forward.z, 2);

        float newmoveX = (float)System.Math.Round(moveStatus.x, 2);
        float newmoveY = (float)System.Math.Round(moveStatus.y, 2);


        client.SendPos(new Vector3(newPosX, newPosY, newPosZ), new Vector3(newforwardX, newforwardY, newforwardZ), new Vector2(newmoveX, newmoveY));
    }

    public void Login()
    {
        client = new ChatClient();

        //虛擬機
        //connectSucceed = client.Connect("34.80.167.143", 4099);

        //local
        connectSucceed = client.Connect("127.0.0.1", 4099);

        //做假事件
        client.tranmitter.Register(0, (tranmitter, message) => { });
        client.tranmitter.Register(1, (tranmitter, message) => { });
        client.tranmitter.Register(2, (tranmitter, message) => { });
        client.tranmitter.Register(3, (tranmitter, message) => { });

        if (connectSucceed)
        {
            Debug.Log("connect");
            string account = GameObject.Find("Account").GetComponent<InputField>().text;
            string password = GameObject.Find("Password").GetComponent<InputField>().text;
            client.SendAccount(account, password);
            client.tranmitter.Register(0, EnterGameScence);
        }
    }

    /// <summary>
    /// 調整攻擊(暫時不傳送)
    /// </summary>
    public void SetAttack(bool _attack)
    {
        //client.tranmitter.mMessage.msgType = 3;
        //Debug.Log("sendAttack");
        client.tranmitter.mMessage.myAttackStatus = _attack;
        client.tranmitter.Send();
    }

    public void NoServerEnter()
    {
        SceneManager.LoadScene(1);
    }

    private void EnterGameScence(Common.Tranmitter _tranmitter, Message03 _player)
    {
        Debug.Log("load");
        SceneManager.LoadScene(1);
    }



}
