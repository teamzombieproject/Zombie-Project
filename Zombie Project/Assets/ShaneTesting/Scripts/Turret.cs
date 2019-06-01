using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float turretHealth;
    public List<ZombieAI> zombieAIScript;
    public GameObject zombieTarget;

    
    void Start()
    {
        zombieTarget = GameObject.FindGameObjectWithTag("Zombie");
    }

    
    void Update()
    {
        if (turretHealth <= 0)
        {
            Debug.Log("destroy me");
            Destroy(this.gameObject);
            for (int i=0; i<zombieAIScript.Count; i++)
            {
                zombieAIScript[i].turretBeingAttacked = false;

            }

        }
    }

   // private void OnTriggerEnter(Collider other)
   // {
   //     if (other.gameObject.tag == "Zombie")
   //     {
   //         (zombieTarget.transform);
    //    }





  //  }
}
