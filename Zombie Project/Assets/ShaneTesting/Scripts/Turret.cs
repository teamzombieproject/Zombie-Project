using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float turretHealth;
    public ZombieAI zombieAIScript;
   

    
    void Start()
    {
        zombieAIScript = GetComponent<ZombieAI>();
    }

    
    void Update()
    {
        if (turretHealth <= 0)
        {
            Debug.Log("destroy me");
            Destroy(this.gameObject);
            zombieAIScript.turretBeingAttacked = false;

        }
    }
}
