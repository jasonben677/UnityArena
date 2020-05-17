using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalManager : MonoBehaviour
{
    static public NumericalManager instance;

    private PlayerInfo gameInfo;

    private PlayerInfo bossInfo;

    private PlayerInfo spider;

    private PlayerInfo[] npcs;

    private PlayerInfo[] strongNpcs;


    [System.NonSerialized]
    public int rp = 5;
    [System.NonSerialized]
    public int bp = 3;


    [SerializeField] ScenceFade ScenceFade;
    [SerializeField] AudioClip[] audio; 


    private void Awake()
    {
        Debug.Log("test");
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
        _InitInformation();
    }

    public PlayerInfo GetMainPlayer()
    {
        return gameInfo;
    }

    public PlayerInfo GetNpc(int _index)
    {
        return npcs[_index];
    }

    public PlayerInfo GetBoss()
    {
        return bossInfo;
    }

    public PlayerInfo GetSpider()
    {
        return spider;
    }

    public PlayerInfo GetStrongNpc(int _index)
    {
        return strongNpcs[_index];
    }

    /// <summary>
    /// 打死小怪
    /// </summary>
    /// <param name="_index">小怪編號 </param>
    /// <param name="_type">怪物類型 0: boss, 1: strongNpc, 2: spider, 3: npc </param>
    public void GetExp(int _index, int _type)
    {
        float exp = 0;

        switch (_type)
        {
            case 0:
                exp = bossInfo.fCurrExp;
                break;

            case 1:
                exp = strongNpcs[_index].fCurrExp;
                break;

            case 2:
                exp = spider.fCurrExp;
                break;

            case 3:
                exp = npcs[_index].fCurrExp;
                break;

            default:
                break;
        }

        gameInfo.fCurrExp += exp;

        if (gameInfo.fCurrExp >= gameInfo.fNextLevelExp)
        {
            StartCoroutine(_PlayerLevelUp());
        }
    }

    /// <summary>
    /// 打死boss
    /// </summary>
    public void GetExp()
    {
        gameInfo.fCurrExp += bossInfo.fCurrExp;
        if (gameInfo.fCurrExp >= gameInfo.fNextLevelExp)
        {
            StartCoroutine(_PlayerLevelUp());
        }
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void LeaveGame()
    {
        Application.Quit();
    }






    /// <summary>
    /// 場景Fade in
    /// </summary>
    public void ScenceFadeIn()
    {
        ScenceFade.FadeIn();
    }


    /// <summary>
    /// 場景Fade Out
    /// </summary>
    public void ScenceFadeOut(int _index)
    {
        ScenceFade.FadeOut(_index);
    }

    /// <summary>
    /// 設定老鼠怪數值
    /// </summary>
    public void SetMouseNpc(int _count)
    {
        //Npc
        npcs = new PlayerInfo[_count];
        for (int i = 0; i < _count; i++)
        {
            npcs[i] = new PlayerInfo();
            npcs[i].sName = "老鼠怪";
            npcs[i].iLevel = 1;
            npcs[i].fPlayerMaxHp = 80;
            npcs[i].fPlayerHp = 80;

            npcs[i].fPlayerMaxMp = 0;
            npcs[i].fPlayerMp = 0;
              
            npcs[i].fAtk = 15;
            npcs[i].fCurrExp = 200;
            npcs[i].fNextLevelExp = 0;
        }
    }


    /// <summary>
    /// 設定老鼠戰士數值
    /// </summary>
    public void SetStrongNpc(int _count)
    {
        strongNpcs = new PlayerInfo[_count];

        for (int i = 0; i < _count; i++)
        {
            strongNpcs[i] = new PlayerInfo();
            strongNpcs[i].sName = "老鼠戰士";
            strongNpcs[i].iLevel = 5;
            strongNpcs[i].fPlayerMaxHp = 350;
            strongNpcs[i].fPlayerHp = 350;

            strongNpcs[i].fPlayerMaxMp = 0;
            strongNpcs[i].fPlayerMp = 0;

            strongNpcs[i].fAtk = 35;
            strongNpcs[i].fCurrExp = 600;
            strongNpcs[i].fNextLevelExp = 0;
        }
    }


    /// <summary>
    /// 設定老鼠酋長數值
    /// </summary>
    public void SetBoss()
    {
        bossInfo = new PlayerInfo()
        {
            iLevel = 10,
            sName = "老鼠酋長",
            fPlayerMaxHp = 1000,
            fPlayerHp = 1000,

            fPlayerMaxMp = 0,
            fPlayerMp = 0,

            fAtk = 55,
            fCurrExp = 1200,
            fNextLevelExp = 0
        };
    }

    public void SetSpider()
    {
        spider = new PlayerInfo()
        {
            iLevel = 5,
            sName = "漆黑蜘蛛",
            fPlayerMaxHp = 450,
            fPlayerHp = 450,

            fPlayerMaxMp = 0,
            fPlayerMp = 0,

            fAtk = 35,
            fCurrExp = 700,
            fNextLevelExp = 0
        };
    }

    /// <summary>
    /// 使用技能
    /// </summary>
    /// <returns></returns>
    public bool UseSkill()
    {
        if (gameInfo.fPlayerMp >= 25f)
        {
            gameInfo.fPlayerMp -= 25;
            return true;
        }
        else 
        {
            return false;
        }
    }

    /// <summary>
    /// 重置玩家數值
    /// </summary>
    public void ResetPlayerInfo()
    {
        PlayerInfo player = gameInfo;
        player.fPlayerHp = player.fPlayerMaxHp;

        player.fPlayerMp = player.fPlayerMaxMp;
    }

    /// <summary>
    /// 重置老鼠怪數值
    /// </summary>
    public void ResertEnemy()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].sName = "老鼠怪";
            npcs[i].iLevel = 1;
            npcs[i].fPlayerMaxHp = 80;
            npcs[i].fPlayerHp = 80;

            npcs[i].fPlayerMaxMp = 0;
            npcs[i].fPlayerMp = 0;

            npcs[i].fAtk = 15;
            npcs[i].fCurrExp = 200;
            npcs[i].fNextLevelExp = 0;
        }
    }

    private void _InitInformation()
    {
        //MainPlayer
        gameInfo = new PlayerInfo()
        {
            iLevel = 1,

            fPlayerMaxHp = 600,
            fPlayerHp = 600,

            fPlayerMaxMp = 50,
            fPlayerMp = 50,

            fAtk = 30,
            fCurrExp = 0,
            fNextLevelExp = 300
        };


    }

    private IEnumerator _PlayerLevelUp()
    {
        PlayerInfo player = gameInfo;
        AudioSource.PlayClipAtPoint(audio[0], PlayerUI.UIManager.instance.mainCamera.transform.position);
        StartCoroutine(PlayerUI.UIManager.instance.ShowLevelUpUI(player.iLevel + 1));

        yield return new WaitForSeconds(0.3f);
        player.iLevel += 1;

        player.fPlayerMaxHp += 10;
        player.fPlayerHp = gameInfo.fPlayerMaxHp;

        player.fPlayerMaxMp += 5;
        player.fPlayerMp = gameInfo.fPlayerMaxMp;

        player.fAtk += 5;

        player.fCurrExp = Mathf.Clamp((player.fCurrExp - player.fNextLevelExp), 0, player.fNextLevelExp);
        float exp = 300;
        player.fNextLevelExp = exp * player.iLevel;

    }

    

}

