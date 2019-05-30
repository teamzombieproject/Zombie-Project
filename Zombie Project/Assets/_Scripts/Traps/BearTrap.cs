using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public int uses = 3;

    public ZombieAI targetHealth;

    public Animator animator;

    public float triggered = 0f;
    public float coolDown = 5f;

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
            animator.SetBool("Destroyed", true);
            isDestroyed = true;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            if (isDestroyed == false && coolDownComplete == true)
            {
                animator.SetBool("Triggered", true);
                targetHealth = other.gameObject.GetComponent<ZombieAI>();
                triggered = 0f;
                coolDownComplete = false;
                uses -= 1;
                TakeHealth();
            }
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
        animator.SetBool("Destroyed", false);
    }
}
