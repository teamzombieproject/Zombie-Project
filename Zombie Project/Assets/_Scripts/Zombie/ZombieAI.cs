using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public float attackRate = 1.0f;
    public float attackTimer;
    public float damageToBarricade = 10;
    public float damageToTurret = 10;
   // public float damageToMachGunTurret = 10;
    public float damageToRadio = 10;
    public float damageToPlayer = 20;
    public Rigidbody rb;
    public bool isHit = false;
    float hitTimer;
    public float stunTime = 0.5f;


    private void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Radio");
        zombieAnimator.SetBool("attack", false);
        rb = GetComponent<Rigidbody>();
        zombieAcceleration = zombieWalkSpeed;                       // set movement speed
        sprite = GetComponentInChildren<SpriteRenderer>();

    }


    private void FixedUpdate()
    {
        if (currentTarget != null && !isHit)  //if there is a target identified from Zombie Detect Script
        {

            Vector3 lookPos = currentTarget.transform.position - transform.position;            // find target direction
            lookPos.y = 0;                                                                      // cancel out y vector

            /*
            var step = zombieRotationSpeed * Time.deltaTime;                                    // calculate zombie rotation speed for this step

            Quaternion rot = Quaternion.LookRotation(lookPos);                                  // convert target dir to rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, step);               // apply rotation
            */



            rb.AddRelativeForce(lookPos * zombieAcceleration, ForceMode.Impulse);       // apply acceleration

            if (rb.velocity.magnitude > zombieMaxSpeed)                                         // clamp max speed
            {
                rb.velocity = rb.velocity.normalized * zombieMaxSpeed;
            }

        }
    }

    void Update()
    {
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

        if (zombieHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        if (attackObject != null)
        {
            ZombieAttack();
        }
        else
        {
            ZombieWalk();
        }

        if (rb.velocity.x < 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }

        if (rb.velocity.z < 0)
        {
            zombieAnimator.SetBool("back", true);
        }
        else
        {
            zombieAnimator.SetBool("back", false);
        }

        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Barricade" ||
            col.gameObject.tag == "Turret" ||
          //  col.gameObject.tag == "MachGunTurret" ||
            col.gameObject.tag == "Radio" ||
            col.gameObject.tag == "Player")
        {
            Debug.Log(gameObject + " collided with " + attackObject);
            attackObject = col.gameObject;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        attackObject = null;
    }

    void ZombieAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
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
            }
           // else if (attackObject.tag == "MachGunTurret")
           // {
            //    TurretMachGun mTurret = attackObject.GetComponent<TurretMachGun>();
            //    mTurret.zombieAIScript.Add(this);
            //    mTurret.machGunTurretHealth -= damageToMachGunTurret;
           // }
            else if (attackObject.tag == "Radio")
            {
                Radio radio = attackObject.GetComponent<Radio>();
                radio.zombieAIScript.Add(this);
                radio.radioHealth -= damageToRadio;
            }
            else if (attackObject.tag == "Player")
            {
               attackObject.GetComponent<Health>().TakeDamage(damageToPlayer, transform.position);  
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
        zombieAcceleration = 0;
        zombieAnimator.SetBool("attack", false);
    }


    void ZombieWalk()
    {
        zombieAcceleration = zombieWalkSpeed;
        zombieAnimator.SetBool("attack", false);
    }


}
