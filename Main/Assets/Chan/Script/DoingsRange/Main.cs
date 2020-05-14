using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main m_Instance;

    public List<Obstacle> m_Obstacles;
    public GameObject m_Player;
    GameObject[] gos;
    // Start is called before the first frame update

    private void Awake()
    {
        m_Instance = this;
        m_Obstacles = new List<Obstacle>();

        gos= GameObject.FindGameObjectsWithTag("Obstacle");

    }

    void Start()
    {
        if (gos != null || gos.Length > 0) 
        {
            foreach (GameObject go in gos) 
            {
                m_Obstacles.Add(go.GetComponent<Obstacle>());
            }
        }
     
    }


    public GameObject GetPlayer() 
    {
        return m_Player;
    }

    public List<Obstacle> GetObstacles() 
    {
        return m_Obstacles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
