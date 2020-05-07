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
                playerHPText.text = LoginManager.instance.client.tranmitter.mMessage.myHp.ToString();
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
                Debug.Log("origin : " + enemyUIMatch.Count);
                enemyUIMatch.Remove(item);
                Debug.Log("Final : " + enemyUIMatch.Count);
            }
            UpdatePlayerCount();
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
        public void ShowPlayerHp(HealthPoint _player)
        {
            float hpRate = (float)System.Math.Round((_player.HP / _player.MaxHP), 2);
            imgPlayerHP.fillAmount = Mathf.Clamp(hpRate, 0.05f, 1f);
        }

        /// <summary>
        /// 敵人UI更新
        /// </summary>
        public void HitPlayer(GameObject _player)
        {
            HealthPoint temp = _player.GetComponent<HealthPoint>();

            Debug.Log(_player.name);

            if (_player.layer == 10)
            {
                if (!enemyUIMatch.ContainsKey(_player))
                {
                    enemyUIMatch.Add(_player, new EnemyUI());
                    enemyUIMatch[_player].GetHealthBar(_player.transform, CheckUIPool());
                }

                if (temp.tag == "ServerSYNC")
                {
                    enemyUIMatch[_player].Name = LoginManager.instance.ShowFriendName(temp.transform.GetSiblingIndex());
                }
                else
                {
                    enemyUIMatch[_player].Name = "雜魚";
                }

                enemyUIMatch[_player].ShowHP(temp);
            }
            else if (_player.layer == 11)
            {
                ShowPlayerHp(temp);
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

            int leftPlayer = LoginManager.instance.client.tranmitter.mMessage.playerLeft;
            texPlayerLeft.text = leftPlayer.ToString();
            if (LoginManager.instance.client.tranmitter.mMessage.gameStart)
            {
                Debug.Log("還剩 " + leftPlayer + "人");
                if (leftPlayer <= 1)
                {
                    Debug.Log("恭喜!! 你是贏家");
                    transform.GetChild(5).gameObject.SetActive(true);
                }
            }
        }

    }
}

