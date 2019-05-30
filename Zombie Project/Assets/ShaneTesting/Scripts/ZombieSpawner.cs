using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombie;
    public float spawnTime;
    public float spawnRepeatRate;
    public Transform[] spawnPointsArray;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnRepeatRate);
    }

   void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPointsArray.Length);
        Instantiate(zombie, spawnPointsArray[spawnPointIndex].position, spawnPointsArray[spawnPointIndex].rotation);

    }
    
    
}
