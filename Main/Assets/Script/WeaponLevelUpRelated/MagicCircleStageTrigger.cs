using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleStageTrigger : MonoBehaviour
{
    private ParticleSystem magicCircleFX;

    [SerializeField] private GameObject treasure;
    private float treasureStartHeight;
        
    private GameObject weaponShineFX;
    private GameObject shockFX;
    private GameObject prefabShockFX;
    

    //Start is called before the first frame update
    void Start()
    {
        magicCircleFX = GetComponentInChildren<ParticleSystem>();
        //Debug.Log(magicCircleFX.gameObject.name);
        treasureStartHeight = treasure.transform.position.y;
        prefabShockFX = Resources.Load("ShockFX") as GameObject;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            if (magicCircleFX != null) 
            {
                Destroy(magicCircleFX.gameObject);
                CreateShockFX();
            }    
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            DropTreasure();            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        {
            //生成武器發光特效
            GameObject whR = other.gameObject.GetComponent<ActorManager>().wm.whR;
            GameObject prefabFX = Resources.Load("WeaponShineFX") as GameObject;
            weaponShineFX = GameObject.Instantiate(prefabFX);
            weaponShineFX.transform.parent = whR.transform;
            weaponShineFX.transform.localPosition = Vector3.zero;
            weaponShineFX.transform.localRotation = Quaternion.identity;
        }
        WeaponController.weaponLevelUp = true;
    }
    

    private void CreateShockFX() 
    {
        //Debug.Log("shock!!!!");
        //GameObject prefabFX = Resources.Load("ShockFX") as GameObject;
        //shockFX = GameObject.Instantiate(prefabFX);
        shockFX = GameObject.Instantiate(prefabShockFX);
        shockFX.transform.parent = transform;
        shockFX.transform.localPosition = Vector3.zero;
        shockFX.transform.localRotation = Quaternion.identity;
    }

    public void DropTreasure()
    {
        if (treasure != null)
        {
            if (treasure.transform.position.y >= treasureStartHeight - 4f)
            {
                treasure.transform.position = new Vector3(treasure.transform.position.x, treasure.transform.position.y - 0.06f, treasure.transform.position.z);
            }
            else
            {
                Destroy(treasure, 1f);
            }
        }
    }
}
