using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
 {
    public float barricadeHealth;
    public ZombieAI zombieAIScript;
    
    
    private void Start()
    {
        zombieAIScript = GetComponent<ZombieAI>();

    }


   void Update()
    {
        if (barricadeHealth <= 0)
            {
                Debug.Log("destroy me");
                Destroy(this.gameObject);
               zombieAIScript.barricadeBeingAttacked = false;
                
            }
        
    }

}
