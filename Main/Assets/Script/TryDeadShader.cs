using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryDeadShader : MonoBehaviour
{
    [SerializeField]
    private Material dead;
    private SkinnedMeshRenderer[] smrList;

    // Start is called before the first frame update
    void Start()
    {
        smrList = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            foreach (var smr in smrList)
            {
                smr.material = dead;
            }
        }
    }
}
