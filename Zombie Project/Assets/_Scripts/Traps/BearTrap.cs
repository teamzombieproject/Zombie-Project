using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public int uses = 3;

    public ZombieAI targetHealth;

    public Animator animator;

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

        CheckAnimationEnd();

        if (triggered >= coolDown)
        {
            coolDownComplete = true;
        }


    }

    private void CheckAnimationEnd()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Trap") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("Triggered", false);
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
            animator.SetBool("Triggered", true);
            TakeHealth();
            triggered = 0f;
            coolDownComplete = false;
            uses -= 1;
        }
    }

    private void TakeHealth()
    {
        targetHealth.zombieHealth -= 100;
    }



    private void Repaired()
    {
        uses = 3;
        isDestroyed = false;
        coolDownComplete = true;
        // Set sprite back to normal
    }
}
