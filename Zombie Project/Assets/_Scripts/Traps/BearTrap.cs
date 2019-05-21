using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public int uses = 3;

    public float triggered = 0f;
    public float coolDown = 2f;
    public float damage = 20f;

    public bool coolDownComplete = true;
    public bool isDestroyed = false;

    // Update is called once per frame
    void Update()
    {
        Destroyed();

        triggered += Time.deltaTime;

        if (triggered >= coolDown)
        {
            coolDownComplete = true;
        }


    }

    private void Destroyed()
    {
        if(uses <= 0)
        {
            // change to destroyed sprite
            isDestroyed = true;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed == false && coolDownComplete == true)
        {
            // Initiate bear trap animation or sprite
            TakeHealth();
            triggered = 0f;
            coolDownComplete = false;
            uses -= 1;
        }
    }

    private void TakeHealth()
    {
        //targetHealth.TakeDamage(damage);
    }

    private void Repaired()
    {
        uses = 3;
        isDestroyed = false;
        coolDownComplete = true;
        // Set sprite back to normal
    }
}
