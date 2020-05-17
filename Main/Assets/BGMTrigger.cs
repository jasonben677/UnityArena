using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTrigger : MonoBehaviour
{

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
       
    private void OnTriggerEnter(Collider other)
    { 
        if(other.tag == "Player")
        {
            SwitchBGM.instance.audioSource.clip = SwitchBGM.instance.audioClips[1];
            SwitchBGM.instance.audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
