using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombie;
    public float spawnTime;
    public float spawnRepeatRate;
    public Transform[] spawnPointsArray;
    bool mayspawn;
    GameManager gm;



    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnRepeatRate);

        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.zombiesAlive++;

    }

   void Spawn()
    {
        if (mayspawn == true)
        {
            int spawnPointIndex = Random.Range(0, spawnPointsArray.Length);
            Instantiate(zombie, spawnPointsArray[spawnPointIndex].position, spawnPointsArray[spawnPointIndex].rotation);
        }

    }
    
    
}
