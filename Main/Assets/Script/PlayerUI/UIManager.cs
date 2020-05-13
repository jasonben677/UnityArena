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
        [SerializeField] TextMeshProUGUI playerMPText;

        [SerializeField] Image xpBar;
        [SerializeField] TextMeshProUGUI levelText;

        private void Awake()
        {
            instance = this;
            mainCamera = Camera.main;
        }

        private void Start()
        {
            NumericalManager.instance.ScenceFadeIn();
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
            UpdatePlayerUI();
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
        public void UpdatePlayerUI()
        {
            PlayerInfo player = NumericalManager.instance.GetMainPlayer();

            if (player.fPlayerHp >= 0)
            {
                float hpRate = (player.fPlayerHp / player.fPlayerMaxHp);

                imgPlayerHP.fillAmount = Mathf.Clamp(hpRate, 0.05f, 1f);
                playerHPText.text = player.fPlayerHp + "/" + player.fPlayerMaxHp;
            }
            else
            {
                imgPlayerHP.fillAmount = 0;
                playerHPText.text = 0.ToString();
            }

            //mp
            float mpRate = (player.fPlayerMp / player.fPlayerMaxMp);
            playerMP.fillAmount = Mathf.Clamp(mpRate, 0.05f, 1f);
            playerMPText.text = player.fPlayerMp + "/" + player.fPlayerMaxMp;

            //exp
            float expRate = (player.fCurrExp / player.fNextLevelExp);
            xpBar.fillAmount = expRate;
            levelText.text = player.iLevel.ToString();

        }

        /// <summary>
        /// 敵人UI更新
        /// </summary>
        public void HitPlayer(GameObject _player)
        {
            //enemy layer
            if (_player.layer == 10)
            {

                if (!enemyUIMatch.ContainsKey(_player))
                {
                    enemyUIMatch.Add(_player, new EnemyUI());
                    enemyUIMatch[_player].GetHealthBar(_player.transform, CheckUIPool());
                }

                if (_player.tag == "Npc")
                {
                    enemyUIMatch[_player].ShowNpcHp(_player.transform.GetSiblingIndex());
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

    }
}

