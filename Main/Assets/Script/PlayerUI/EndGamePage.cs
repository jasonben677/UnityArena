using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerLeft;
    [SerializeField] TextMeshProUGUI titleInfo;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        NumericalManager.instance.ScenceFadeIn();
    }


    public void BackToLogin()
    {
        NumericalManager.instance.ScenceFadeOut();
    }

}
