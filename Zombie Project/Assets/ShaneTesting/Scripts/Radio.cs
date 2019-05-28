using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public ZombieAI zombieAIScript;
    public float radioHealth;


    // Start is called before the first frame update
    void Start()
    {
        zombieAIScript = GetComponent<ZombieAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (radioHealth <= 0)
        {
            Debug.Log("destroy me");
            Destroy(this.gameObject);
            zombieAIScript.radioBeingAttacked = false;

        }
    }
}
