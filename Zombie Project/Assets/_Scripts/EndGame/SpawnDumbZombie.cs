using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDumbZombie : MonoBehaviour
{
    public GameObject dumbZombie;
    float timer = 0, timePast;
    public float randomMax = 7, randomMin = 5;
    

    private void Start()
    {
        timePast = Random.Range(randomMin, randomMax);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timePast)
        {
            timer = 0;
            Instantiate(dumbZombie, transform.position, Quaternion.identity);
        }
    }
}
