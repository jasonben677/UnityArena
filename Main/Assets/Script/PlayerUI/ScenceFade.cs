using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceFade : MonoBehaviour
{
    public Animator anim;


    public void LoadScence()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }

    public void FadeIn()
    {
        anim.ResetTrigger("FadeOut");
        anim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeIn");
        anim.SetTrigger("FadeOut");
    }
}
