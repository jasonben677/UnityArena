using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private GameObject whR;

    [Header("==== Audio ====")]
    [SerializeField]
    private AudioClip swingSound;
    [SerializeField]
    private AudioClip explosionSound;
    [SerializeField]
    private AudioClip footStepSound;
    //[SerializeField]
    //private AudioClip runStepSound;

    // Start is called before the first frame update
    void Start()
    {
        whR = transform.gameObject.GetComponent<WeaponManager>().whR;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnFootStep()
    {
        AudioSource.PlayClipAtPoint(footStepSound, transform.parent.transform.position, 1);
    }

    //public void OnRunStep()
    //{
    //    AudioSource.PlayClipAtPoint(runStepSound, transform.parent.transform.position, 1);
    //}

    public void OnSwingSound()
    {
        AudioSource.PlayClipAtPoint(swingSound, whR.transform.position, 1);
    }

    public void OnExplosion() 
    {
        //AudioSource.PlayClipAtPoint(explosionSound, whR.transform.position, 0.3f);
    }
}
