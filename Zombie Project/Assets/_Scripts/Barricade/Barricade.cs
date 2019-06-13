using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
 {
    public float barricadeHealth;
    public List<ZombieAI> zombieAIScript;
    
    
    
    private void Start()
    {
       

    }


   void Update()
    {
        if (barricadeHealth <= 0)
            {
                Debug.Log("destroy me");
                Destroy(this.gameObject);
            for (int i = 0; i < zombieAIScript.Count; i++)
            {
                //zombieAIScript[i].barricadeBeingAttacked = false;
                zombieAIScript[i].attackObject = null;

            }

        }
        
    }

}
