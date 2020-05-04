using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour
{
    public GameObject target;
    public int m_iamount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vRandom = new Vector3(Random.Range(-30f, 30f), 10, Random.Range(-30f, 30f));
        if (0 < m_iamount)
        {
            Instantiate(target, vRandom, new Quaternion(0, 0, 0, 0), this.transform);
            m_iamount--;

        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 50);
    }
}
