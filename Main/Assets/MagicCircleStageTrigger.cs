using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleStageTrigger : MonoBehaviour
{
    private ParticleSystem magicCircleFX;

    [SerializeField] private GameObject treasureFX;
    private float treasureStartHeight;
        
    private GameObject weaponShineFX;
    private GameObject shockFX;

    //Start is called before the first frame update
    void Start()
    {
        magicCircleFX = GetComponentInChildren<ParticleSystem>();
        //Debug.Log(magicCircleFX.gameObject.name);
        treasureStartHeight = treasureFX.transform.position.y;
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
                Destroy(magicCircleFX.gameObject, 0.3f);
            }
            WeaponController.weaponLevelUp = true;
            CreateShockFX();
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
    }
    

    private void CreateShockFX() 
    {
        //Debug.Log("shock!!!!");
        GameObject prefabFX = Resources.Load("ShockFX") as GameObject;
        shockFX = GameObject.Instantiate(prefabFX);
        shockFX.transform.parent = transform;
        shockFX.transform.localPosition = Vector3.zero;
        shockFX.transform.localRotation = Quaternion.identity;
    }

    public void DropTreasure()
    {
        if (treasureFX.transform.gameObject != null)
        {
            if (treasureFX.transform.position.y >= treasureStartHeight - 4f)
            {
                treasureFX.transform.position = new Vector3(treasureFX.transform.position.x, treasureFX.transform.position.y - 0.1f, treasureFX.transform.position.z);
            }
            else 
            {
                Destroy(treasureFX.transform.gameObject, 2f);
            }
        }
    }
}
