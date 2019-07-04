using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public GameObject currentTarget;
    public float zombieWalkSpeed = 0.25f;
    public float zombieRotationSpeed = 1.0f;
    public float zombieMaxSpeed = 2.0f;
    float zombieAcceleration;
    public float zombieHealth = 100;
    public Animator zombieAnimator;
    SpriteRenderer sprite;
   
    public GameObject attackObject;                             // use a single object to handle attacking instead of multiple bools/objects
    
    public float attackRate = 0.8f;
    public float attackTimer;
    public float damageToBarricade = 10;
    public float damageToTurret = 10;
    public float damageToRadio = 10;
    public float damageToPlayer = 20;
    public Rigidbody rb;
    public bool isHit = false;
    float hitTimer;
    public float stunTime = 0.5f;

    [HideInInspector]
    public AudioSource audioSrc;
    public AudioClip zombieAttack;
   

    // public GameObject radioHealthBar;

    //CONNORS NEW CODE
    [HideInInspector]
    public NavMeshAgent navAgent;

    public bool ended, attack;
     
    private void Start()
    {
        //currentTarget = GameObject.FindGameObjectWithTag("Radio");
        zombieAnimator.SetBool("attack", false);
        rb = GetComponent<Rigidbody>();
        zombieAcceleration = zombieWalkSpeed;                       // set movement speed
        sprite = GetComponentInChildren<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = zombieAttack;

        //CONNORS NEW CODE
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = zombieMaxSpeed;
        navAgent.SetDestination(GameObject.FindGameObjectWithTag("Radio").transform.position);
        // navAgent.acceleration = zombieWalkSpeed;

    }


   /* private void FixedUpdate()
    {
        if (currentTarget != null && !isHit)  //if there is a target identified from Zombie Detect Script
        {

             Vector3 lookPos = currentTarget.transform.position - transform.position;            // find target direction
             lookPos.y = 0;                                                                      // cancel out y vector


             rb.AddRelativeForce(lookPos * zombieAcceleration, ForceMode.Impulse);       // apply acceleration

             if (rb.velocity.magnitude > zombieMaxSpeed)                                         // clamp max speed
             {
                 rb.velocity = rb.velocity.normalized * zombieMaxSpeed;
             }
             

            //moving toward target code
            navAgent.SetDestination(currentTarget.transform.position);
            
        }
    }*/

    void Update()
    {
        if (currentTarget != null && currentTarget.transform.position != navAgent.destination)  //if there is a target identified from Zombie Detect Script
        {
            navAgent.SetDestination(currentTarget.transform.position);
            
        }
            if (isHit)                                                                              // stuns the zombie for a moment when hit (so it stops moving)
        {
            ZombieHit();                                                                        // stops the zombie from moving/changes animation

            hitTimer -= Time.deltaTime;                                                         // coundown the hitTimer
            if (hitTimer <= 0)                                                                  // if the hitTimer has expired
            {
                isHit = false;                                                                  // turn off the flag
                hitTimer = stunTime;                                                            // reset the timer
            }
        }
        /* done through zombie health script for some reason
        if (zombieHealth <= 0)
        {
            Destroy(gameObject);
            
        }
        */
        
        if (attackObject != null)
        {
            ZombieAttack();
        }
        else
        {
            ZombieWalk();
        }

        //if (navAgent.velocity.x < 0) //rb is now navAgent
        if (currentTarget != null)
        {
            if (transform.position.x > currentTarget.transform.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }

            //if (navAgent.velocity.z < 0) //rb is now navagent
            if (transform.position.z > currentTarget.transform.position.z)
            {
                zombieAnimator.SetBool("back", true);
            }
            else
            {
                zombieAnimator.SetBool("back", false);
            }
        }
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Barricade" ||
            col.gameObject.tag == "Turret" ||
            col.gameObject.tag == "Radio" ||
            col.gameObject.tag == "Player")
        {
            attackObject = col.gameObject;
            Debug.Log(gameObject + " collided with " + attackObject);
        }

    }

   private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject == attackObject)
       // attackObject = null;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == attackObject)
        {
            attackObject = null;
            attackTimer = 0;
        }
    }

    void ZombieAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            audioSrc.clip = zombieAttack;
            audioSrc.Play();
            //GetComponentInChildren<TurnToLook>().enabled = true;
            //   GetComponentInChildren<TurnToLook>().EnableTurn(attackObject.transform, true);
            if (attackObject.tag == "Barricade")
            {
                Barricade barricade = attackObject.GetComponent<Barricade>();
                barricade.zombieAIScript.Add(this);
                barricade.barricadeHealth -= damageToBarricade;
            }
            else if (attackObject.tag == "Turret")
            {
                Turret turret = attackObject.GetComponent<Turret>();
                turret.zombieAIScript.Add(this);
                turret.turretHealth -= damageToTurret;
                navAgent.velocity += (attackObject.transform.position - transform.position).normalized * Mathf.Clamp(Vector3.Distance(attackObject.transform.position, transform.position), 3, 5);
            }
           
            else if (attackObject.tag == "Radio")
            {
                Radio radio = attackObject.GetComponent<Radio>();
                radio.zombieAIScript.Add(this);
                radio.radioHealth -= damageToRadio;
            }
            else if (attackObject.tag == "Player")
            {
               attackObject.GetComponent<Health>().TakeDamage(damageToPlayer, transform.position);
               audioSrc.Play();
               navAgent.velocity += (attackObject.transform.position - transform.position).normalized * Mathf.Clamp(Vector3.Distance(attackObject.transform.position, transform.position), 3, 5);
            }
            
            attackTimer = 0;
        }

        ZombieAttackAnimation();
    }

  
    void ZombieAttackAnimation()
    {
        zombieAcceleration = 0;
        zombieAnimator.SetBool("attack", true);
    }

    void ZombieHit()
    {
        //zombieAcceleration = 0;
        navAgent.isStopped = true;
        zombieAnimator.SetBool("attack", false);
    }


    void ZombieWalk()
    {
        //zombieAcceleration = zombieWalkSpeed;
        navAgent.isStopped = false;
        zombieAnimator.SetBool("attack", false);
    }


}
