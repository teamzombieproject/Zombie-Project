using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
   
    public float zombieHealthTest = 100f;
    GameManager  gm;
    


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.zombiesAlive++;
    }

    public void TakeDamage(float amount)
    {
        zombieHealthTest -= amount;

        if (zombieHealthTest <= 0)
        {
            gm.zombiesAlive--;
            Destroy(gameObject);
            
        }
    }


}
