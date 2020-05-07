using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : PlayerInput
{
    // Start is called before the first frame update
    //IEnumerator Start()
    //{
    //    //Dup = 1.0f;
    //    //Dright = 0;
    //    while (true)
    //    {
    //        attack = true;
    //        yield return 3;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        UpdateDmagDvec(Dup, Dright);
    }
}
