using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monsterbox : MonoBehaviour
{
    public GameObject target;
    public int m_iamount;
    public int radius;


    // Start is called before the first frame update
    void Start()
    {
        Ins_Obj();
    }

    // Update is called once per frame
    void Update()
    {
        Ins_Obj();


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
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
}
