using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
   
    public float zombieHealth = 100f, deathMagnitude = 5;
    public GameObject corpsePrefab;
    GameManager  gm;
    NavMeshAgent navAgent;




    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.zombiesAlive++;
       
    }

    private void Update()
    {
        if (zombieHealth <= 0)
        {
          gm.zombiesAlive--;
            GameObject corpse = Instantiate(corpsePrefab, transform.position + new Vector3 (0,.1f,0), Quaternion.identity);
            if (GetComponent<NavMeshAgent>().velocity.x > 0)
                corpse.GetComponentInChildren<SpriteRenderer>().flipX = false;
            else corpse.GetComponentInChildren<SpriteRenderer>().flipX = true;
            //corpse.GetComponentInParent<Rigidbody>().velocity = navAgent.velocity * deathMagnitude;
            Destroy(gameObject);

        }
    }
    public void TakeDamage (float amount)
    {
        zombieHealth -= amount;
    }


    public void Hurt(float amount, Vector3 position, float pushMag)
    {
        zombieHealth -= amount;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.velocity += (gameObject.transform.position - position) * pushMag;
       
    }




}
