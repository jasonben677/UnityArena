using DG.Tweening;
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
                //GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
                smr.material = dead;
                //smr.material.DOFloat(1f, "_Step", 2.5f).OnComplete(() => Destroy(clone));
                smr.material.DOFloat(1f, "_Step", 2.5f).OnComplete(() => gameObject.SetActive(false));
            }
        }
    }
}
