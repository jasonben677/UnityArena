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

        [SerializeField] GameObject bossUI;


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
            xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, expRate , 0.2f) ;
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
                if (_player.tag == "Boss")
                {
                    UpdateBoss();
                }
                else
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
        }

        /// <summary>
        /// 敵人UI物件總數量，目前4個
        /// </summary>
        /// <returns></returns>
        public GameObject CheckUIPool()
        {
            GameObject obj;
            for (int i = 0; i < 10; i++)
            {
                obj = transform.GetChild(0).GetChild(i).gameObject;

                if (obj.activeSelf == false)
                {
                    return obj;
                }
            }
            return null;
        }


        public void UpdateBoss()
        {
            PlayerInfo enemy = NumericalManager.instance.GetBoss();

            Image enemyHp = bossUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            TextMeshProUGUI enemyName = bossUI.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hpText = bossUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI enemyLevel = bossUI.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

            float hpRate = (float)System.Math.Round((double)(enemy.fPlayerHp / enemy.fPlayerMaxHp), 2);

            enemyHp.fillAmount = Mathf.Clamp(hpRate, 0.05f, 1f);

            hpText.text = enemy.fPlayerHp + "/" + enemy.fPlayerMaxHp.ToString();

            enemyName.text = enemy.sName;

            enemyLevel.text = enemy.iLevel.ToString();

            if (hpRate <= 0.02f)
            {
                bossUI.SetActive(false);
            }
            else
            {
                bossUI.SetActive(true);
            }
        }

    }
}

