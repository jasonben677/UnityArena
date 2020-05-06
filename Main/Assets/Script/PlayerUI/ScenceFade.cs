using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceFade : MonoBehaviour
{
    public Animator anim;


    public void LoadScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
}
