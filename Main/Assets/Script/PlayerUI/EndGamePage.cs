using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePage : MonoBehaviour
{
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.None;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Time.timeScale = 1;

        NumericalManager.instance.ScenceFadeIn();
        NumericalManager.instance.ResetPlayerInfo();


    }


    public void BackToLogin()
    {
        NumericalManager.instance.ScenceFadeOut(0);
    }

    public void BackToGame()
    {
        NumericalManager.instance.ScenceFadeOut(1);
    }

}
