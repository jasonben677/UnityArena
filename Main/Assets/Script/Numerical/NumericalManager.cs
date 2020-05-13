using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalManager : MonoBehaviour
{
    static public NumericalManager instance;

    private PlayerInfo gameInfo;

    private PlayerInfo[] npcs;

    [SerializeField] ScenceFade ScenceFade;

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

    public void GetExp(int _index)
    {
        gameInfo.fCurrExp += npcs[_index].fCurrExp;
        if (gameInfo.fCurrExp >= gameInfo.fNextLevelExp)
        {
            _PlayerLevelUp();
        }
        //PlayerUI.UIManager.instance.UpdatePlayerUI();
    }


    public void ScenceFadeIn()
    {
        ScenceFade.FadeIn();
    }

    public void ScenceFadeOut()
    {
        ScenceFade.FadeOut();
    }

    public void SetMouseNpc(int _count)
    {
        //Npc
        npcs = new PlayerInfo[_count];
        for (int i = 0; i < _count; i++)
        {
            npcs[i] = new PlayerInfo();
            npcs[i].sName = "老鼠怪";
            npcs[i].iLevel += 1;
            npcs[i].fPlayerMaxHp = 80;
            npcs[i].fPlayerHp = 80;

            npcs[i].fPlayerMaxMp = 0;
            npcs[i].fPlayerMp = 0;
              
            npcs[i].fAtk = 15;
            npcs[i].fCurrExp = 300;
            npcs[i].fNextLevelExp = 0;
        }
    }

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

    private void _InitInformation()
    {

        //MainPlayer
        gameInfo = new PlayerInfo()
        {
            iLevel = 1,

            fPlayerMaxHp = 100,
            fPlayerHp = 100,

            fPlayerMaxMp = 50,
            fPlayerMp = 50,

            fAtk = 20,
            fCurrExp = 0,
            fNextLevelExp = 300
        };


    }

    private void _PlayerLevelUp()
    {
        PlayerInfo player = gameInfo;
        player.iLevel += 1;

        player.fPlayerMaxHp += 10;
        player.fPlayerHp = gameInfo.fPlayerMaxHp;

        player.fPlayerMaxMp += 5;
        player.fPlayerMp = gameInfo.fPlayerMaxMp;

        player.fAtk += 3;

        player.fCurrExp = Mathf.Clamp((player.fCurrExp - player.fNextLevelExp), 0, player.fNextLevelExp);
        float exp = 300;
        player.fNextLevelExp = exp * player.iLevel;

    }

}

