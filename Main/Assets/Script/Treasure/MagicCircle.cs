using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    [SerializeField]
    private Treasure treasure;
    [SerializeField]
    private ParticleSystem magicCircle;
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
        if(magicCircle != null)
        {
        magicCircle.Play();
        Destroy(magicCircle.transform.gameObject, 3.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        treasure.SendMessage("Drop");
    }
}
