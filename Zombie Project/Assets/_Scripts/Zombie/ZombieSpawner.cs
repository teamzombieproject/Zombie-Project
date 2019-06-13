using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombie;
   // public float spawnTime;
    public float spawnRepeatRate;
    public GameObject[] spawnPointsArray;
    public bool mayspawn = true;
    GameManager gm;

    float spawnTimer = 0f;
   



    void Start()
    {
        spawnTimer = 0f;
        spawnPointsArray = GameObject.FindGameObjectsWithTag("ZombieSpawnPoint");
                
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
       
        

    }

    void Update()
    {
        if (mayspawn == true)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnRepeatRate)
            {
                Spawn();
                spawnTimer = 0;
            }
        }

    }

    void Spawn()
    {
        
            int spawnPointIndex = Random.Range(0, spawnPointsArray.Length);
            Instantiate(zombie, spawnPointsArray[spawnPointIndex].transform.position, spawnPointsArray[spawnPointIndex].transform.rotation);
        
    }
    
    
}
