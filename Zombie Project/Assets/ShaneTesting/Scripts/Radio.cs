using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public List<ZombieAI> zombieAIScript;
    public float radioHealth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (radioHealth <= 0)
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
