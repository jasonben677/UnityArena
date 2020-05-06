using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PlayerUI
{
    public class EnemyUI
    {
        public string Name;

        public Transform enemy
        {
            get;
            private set;
        }

        public GameObject healthBar
        {
            get;
            private set;
        }

        public bool IsUse
        {
            get;
            private set;
        }

        public EnemyUI()
        {
            IsUse = false;
        }

        public void GetHealthBar(Transform _enemy, GameObject _bar)
        {
            enemy = _enemy;
            healthBar = _bar;
            IsUse = true;
            healthBar.SetActive(true);
        }

        public void UpdatePos()
        {
            if (enemy != null && healthBar != null)
            {
                healthBar.transform.position = UIManager.instance.mainCamera.WorldToScreenPoint(enemy.position + new Vector3(0f, 2.0f, 0f));
            }

        }

        public void ShowHP(HealthPoint _player)
        {
            if (enemy != null && healthBar != null)
            {
                Image enemyHp = healthBar.transform.GetChild(0).GetChild(0).GetComponent<Image>();
                Text enemyName = healthBar.transform.GetChild(2).GetChild(0).GetComponent<Text>();

                float hpRate = (float)System.Math.Round((_player.HP / _player.MaxHP), 2);

                enemyHp.fillAmount = Mathf.Clamp(hpRate, 0.05f, 1f);

                enemyName.text = Name;


                if (hpRate <= 0.02f)
                {
                    IsUse = false;
                }
            }

        }

        public void EnemyDisable()
        {
            enemy = null;
            healthBar.SetActive(false);
            IsUse = false;
        }

    }
}

