using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceFade : MonoBehaviour
{
    public Animator anim;

    private int iNextScenceIndex = 0;


    public void LoadScence()
    {
        try
        {
            SceneManager.LoadScene(iNextScenceIndex);
        }
        catch (System.Exception)
        {

            Debug.LogError("LoadError");
        }
        
    }

    public void FadeIn()
    {
        anim.ResetTrigger("FadeOut");
        anim.SetTrigger("FadeIn");
    }

    public void FadeOut(int _scenceIndex)
    {
        iNextScenceIndex = _scenceIndex;
        anim.SetTrigger("FadeIn");
        anim.SetTrigger("FadeOut");
    }
}
