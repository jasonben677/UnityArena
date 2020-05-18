using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour
{
    public Button enterGame;
    public Button leaveGame;

    private void Start()
    {
        enterGame.onClick.RemoveAllListeners();
        leaveGame.onClick.RemoveAllListeners();

        enterGame.onClick.AddListener( () => NumericalManager.instance.ScenceFadeOut(1));
        leaveGame.onClick.AddListener(() => NumericalManager.instance.LeaveGame());
    }

}
