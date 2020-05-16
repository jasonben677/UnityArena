using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePage : MonoBehaviour
{
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.None;
        NumericalManager.instance.ScenceFadeIn();
        NumericalManager.instance.ResetPlayerInfo();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }


    public void BackToLogin()
    {
        NumericalManager.instance.LeaveGame();
    }

    public void BackToGame()
    {
        NumericalManager.instance.ScenceFadeOut(1);
    }

}
