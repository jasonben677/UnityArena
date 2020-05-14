using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle02 : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem skillParticle;
    [SerializeField]
    private ParticleSystem magicCircle02;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(skillParticle != null)
        {
            Destroy(skillParticle.transform.parent.gameObject, 1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        magicCircle02.Play();
        Destroy(magicCircle02.transform.gameObject, 1.5f);
    }
}
