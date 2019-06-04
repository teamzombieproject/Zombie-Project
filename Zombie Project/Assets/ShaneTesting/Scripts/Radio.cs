using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public float radioHealth;
    public List<ZombieAI> zombieAIScript;
    


   // Update is called once per frame
    void Update()
    {
        if (radioHealth <= 0)  // make this game over instead of script below
        {
            Debug.Log("destroy me");
            Destroy(this.gameObject); 
            for (int i=0; i<zombieAIScript.Count; i++)
            {
                zombieAIScript[i].radioBeingAttacked = false;
                     
            }

        }
    }
}
