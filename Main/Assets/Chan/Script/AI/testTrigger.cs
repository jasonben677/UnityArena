using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour
{
    GameObject[] Enemy;
    public GameObject vfxult;
    private void Awake()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Npc");

    }
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
        if (other.gameObject.tag == "Player") 
        {
            foreach (GameObject Npc in Enemy)
            {

                    Vector3 TreeEnemy = Npc.transform.position;
                    TreeEnemy.y +=1;
                TreeEnemy.z += 1f;
                    Vector3 vTarget = this.transform.position;

                    float fDis = Vector3.Distance(TreeEnemy, vTarget);

                    Debug.Log(fDis);
                if (fDis < 30)
                {


                    Npc.SetActive(true);
                   Instantiate(vfxult, TreeEnemy, Npc.transform.rotation,Npc.transform);

                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 10);
    }
}
