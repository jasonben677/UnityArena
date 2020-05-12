using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PlayerUI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance = null;

        public GameObject player;

        public Camera mainCamera;

        public Camera MiniMapCamera;

        Dictionary<GameObject, EnemyUI> enemyUIMatch = new Dictionary<GameObject, EnemyUI>();
        List<GameObject> temp;

        [Header("玩家UI")]
        [SerializeField] Image imgPlayerHP;
        [SerializeField] TextMeshProUGUI  playerHPText;
        [SerializeField] Image playerMP;
        [SerializeField] Text texPlayerLeft;

        private void Awake()
        {
            instance = this;
            mainCamera = Camera.main;
        }

        private void Start()
        {
            if (LoginManager.instance != null)
            {
                LoginManager.instance.ScenceFadeIn();
                playerHPText.text = LoginManager.instance.client?.tranmitter.mMessage.myHp.ToString();
            }
               
        }

        private void Update()
        {
            temp = new List<GameObject>();

            //更新UI位置
            foreach (KeyValuePair<GameObject, EnemyUI> item in enemyUIMatch)
            {
                item.Value?.UpdatePos();
                if (item.Value.IsUse == false || item.Key.activeSelf == false)
                {
                    item.Value.EnemyDisable();
                    temp.Add(item.Key);
                }
            }

            //移除沒在使用的物件
            foreach (var item in temp)
            {
                enemyUIMatch.Remove(item);
            }
            UpdatePlayerCount();
            ShowPlayerHp();
        }


        private void LateUpdate()
        {
            Vector3 newMinimapPos = player.transform.position;
            newMinimapPos.y = MiniMapCamera.transform.position.y;
            MiniMapCamera.transform.position = newMinimapPos;
            MiniMapCamera.transform.rotation = Quaternion.Euler(90f, mainCamera.transform.eulerAngles.y, 0);
        }


        /// <summary>
        /// 玩家UI更新
        /// </summary>
        public void ShowPlayerHp()
        {
            TestDll.Message03 player;

            if (LoginManager.instance != null)
            {
                player = LoginManager.instance.client.tranmitter.mMessage;

                if (player.myHp >= 0)
                {
                    float hpRate = (player.myHp / player.myMaxHp);
                    imgPlayerHP.fillAmount = Mathf.Clamp(hpRate, 0.05f, 1f);
                    playerHPText.text = LoginManager.instance.client?.tranmitter.mMessage.myHp.ToString();
                }
                else
                {
                    imgPlayerHP.fillAmount = 0;
                    playerHPText.text = 0.ToString();
                }
            }



        }

        /// <summary>
        /// 敵人UI更新
        /// </summary>
        public void HitPlayer(GameObject _player)
        {
            HealthPoint temp = _player.GetComponent<HealthPoint>();

            //enemy layer
            if (_player.layer == 10)
            {
                Debug.LogError(_player.name);
                Debug.LogError(_player.tag);
                if (!enemyUIMatch.ContainsKey(_player))
                {
                    enemyUIMatch.Add(_player, new EnemyUI());
                    enemyUIMatch[_player].GetHealthBar(_player.transform, CheckUIPool());
                }

                if (_player.tag == "ServerSYNC")
                {
                    enemyUIMatch[_player].Name = LoginManager.instance.ShowFriendName(_player.transform.GetSiblingIndex());
                    enemyUIMatch[_player].ShowHP(_player.transform.GetSiblingIndex());
                }
                else if (_player.tag == "Npc")
                {
                    enemyUIMatch[_player].Name = "雜魚";
                    enemyUIMatch[_player].ShowNpcHp(_player.transform.GetSiblingIndex());
                }
                else
                {
                    enemyUIMatch[_player].Name = "雜魚";
                    enemyUIMatch[_player].ShowHP(temp);
                }
            }
        }

        /// <summary>
        /// 敵人UI物件總數量，目前4個
        /// </summary>
        /// <returns></returns>
        public GameObject CheckUIPool()
        {
            GameObject obj;
            for (int i = 0; i < 4; i++)
            {
                obj = transform.GetChild(0).GetChild(i).gameObject;

                if (obj.activeSelf == false)
                {
                    return obj;
                }
            }
            return null;
        }

        public void UpdatePlayerCount()
        {
            if (LoginManager.instance == null)
            {
                return;
            }

            if (LoginManager.instance.client == null)
            {
                return;
            }

            int leftPlayer = LoginManager.instance.client.tranmitter.mMessage.playerLeft;
            texPlayerLeft.text = leftPlayer.ToString();
            if (LoginManager.instance.client.tranmitter.mMessage.gameStart)
            {
                Debug.Log("還剩 " + leftPlayer + "人");
                if (leftPlayer <= 1)
                {
                    LoginManager.instance.ScenceFadeOut();
                }
            }
        }

    }
}

