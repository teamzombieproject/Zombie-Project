using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
   
    public float zombieHealthTest = 40f;
    GameManager  gm;
    
    


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.zombiesAlive++;
       
    }

    private void Update()
    {
        if (zombieHealthTest <= 0)
        {
          gm.zombiesAlive--;
           Destroy(gameObject);

        }
    }

    public void TakeDamage(float amount)
    {
        zombieHealthTest -= amount;
               
    }




}
