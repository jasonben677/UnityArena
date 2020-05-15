using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monsterbox : MonoBehaviour
{
    public GameObject target;
    public int m_iamount;
    public int radius;

    private AITest[] myMouseNpc;


    private void Awake()
    {
        _GetAllMouseNpc();
    }

    // Start is called before the first frame update
    void Start()
    {
        Ins_Obj();
    }

    // Update is called once per frame
    void Update()
    {
        Ins_Obj();
        _NpcUpdate();
    }



    void Ins_Obj() 
    {
        Vector3 vRandom = new Vector3(Random.Range(-radius, radius), 10, Random.Range(-radius, radius));
        if (0 < m_iamount)
        {
            Instantiate(target, this.transform.position + vRandom, new Quaternion(0, 0, 0, 0), this.transform);
            m_iamount--;

        }
    }

    private void _GetAllMouseNpc()
    {
        myMouseNpc = new AITest[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            myMouseNpc[i] = transform.GetChild(i).GetComponent<AITest>();
        }
        NumericalManager.instance.SetMouseNpc(transform.childCount);
    }

    private void _NpcUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (myMouseNpc[i].gameObject.activeSelf == true)
            {
                myMouseNpc[i].NpcUpdate();
            }

        }
    }
}
