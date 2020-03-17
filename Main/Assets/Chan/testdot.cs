using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdot : MonoBehaviour
{

    //綁在Player身上的空的物件用來計算2k7dj/ j2k7DOT

    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObjectMove();
    }
    void GameObjectMove() 
    {
        if (Input.GetKey(KeyCode.W)){transform.position =  Player.position+ new Vector3(0, 0, 1);}
        if (Input.GetKey(KeyCode.S)){transform.position = Player.position + new Vector3(0, 0, -1);}
        if (Input.GetKey(KeyCode.A)){transform.position = Player.position + new Vector3(-1, 0, 0);}
        if (Input.GetKey(KeyCode.D)){transform.position = Player.position + new Vector3(1, 0, 0);}
        if (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.A)) { transform.position = Player.position + new Vector3(-1, 0, 1); }
        if (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.D)) { transform.position = Player.position + new Vector3(1, 0, 1); }
        if (Input.GetKey(KeyCode.A) & Input.GetKey(KeyCode.S)) { transform.position = Player.position + new Vector3(-1, 0, -1); }
        if (Input.GetKey(KeyCode.S)&Input.GetKey(KeyCode.D)){ transform.position = Player.position + new Vector3(1, 0, -1); }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);

        Gizmos.DrawCube(this.transform.position, new Vector3(1, 1, 1));

    }
}
