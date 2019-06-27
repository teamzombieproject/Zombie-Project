using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public void TakeDamage (float amount)
    {
        zombieHealthTest -= amount;
    }


    public void Hurt(float amount, Vector3 position, float pushMag)
    {
        zombieHealthTest -= amount;
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        navAgent.velocity += (gameObject.transform.position - position) * pushMag;
       
    }




}
