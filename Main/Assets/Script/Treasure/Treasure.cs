using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private float startHeight;
    // Start is called before the first frame update
    void Start()
    {
        startHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop()
    {
        if(transform.position.y >= startHeight - 5.5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
    }
}
