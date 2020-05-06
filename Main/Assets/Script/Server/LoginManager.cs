using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TestDll;

public class LoginManager : MonoBehaviour
{
    static public LoginManager instance;

    public ChatClient client = null;

    [SerializeField] ScenceFade ScenceFade;
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



    public string ShowFriendName(int index)
    {
        if (client == null)
        {
            return null;
        }

        string name = client.tranmitter.mMessage.friend[index].name;
        return name;
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
    /// 調整攻擊
    /// </summary>
    public void SetAttack(bool _attack)
    {
        //client.tranmitter.mMessage.msgType = 3;
        //Debug.Log("sendAttack");
        client.tranmitter.mMessage.myAttackStatus = _attack;
        client.tranmitter.Send();
    }

    /// <summary>
    /// 送出主角資料
    /// </summary>
    /// <param name="_transform">角色transform</param>
    /// <param name="_hp">角色當前hp血量</param>
    /// <param name="_atk">角色攻擊力</param>
    public void SetHP(Transform _transform, float _hp, float _atk)
    {
        if (_transform.gameObject.layer == 10)
        {
            return;
        }
        client.tranmitter.mMessage.myHp = (int)_hp;
        client.tranmitter.mMessage.myAtkDamage = (int)_atk;
    }

    public void NoServerEnter()
    {
        ScenceFadeOut();
    }

    private void EnterGameScence(Common.Tranmitter _tranmitter, Message03 _player)
    {
        if (_player.success)
        {
            ScenceFadeOut();
            Debug.Log("login work");
        }
        else
        {
            Debug.Log("login Fail");
        }
    }


    public void ScenceFadeIn()
    {
        ScenceFade.FadeIn();
    }

    public void ScenceFadeOut()
    {
        ScenceFade.FadeOut();
    }

}
