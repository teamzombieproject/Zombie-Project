using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{ 
    
    public GameObject currentTarget;
    public float zombieSpeed;
    public int zombieHealth = 100;    
    public float rotSpeed;
    public Animator zombieAnimator;
    public Barricade thisBarricade;
    public Turret thisTurret;
    public Radio thisRadio;
    public Movement thisPlayer;
    public bool barricadeBeingAttacked = false;
    public bool turretBeingAttacked = false;
    public bool radioBeingAttacked = false;
    public float attackRate = 1.0f;
    public float attackTimer;
    public float damageToBarricade = 10;
    public float damageToTurret = 10;
    public float damageToRadio = 10;


    private void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Radio");
        zombieAnimator.SetBool("attack", false);

    }


    void Update()
    {
        if (currentTarget != null)  //if there is a targer identified from Zombie Detect Script
        {
            transform.LookAt(currentTarget.transform);  //look at the target
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, zombieSpeed); //move towards the target
        }

        if (barricadeBeingAttacked == true)
        {
            ZombieAttackBarricade();
        }

        if (turretBeingAttacked == true)
        {
            ZombieAttackTurret();
        }

        if (radioBeingAttacked == true)
        {
            ZombieAttackRadio();
        }

        if (barricadeBeingAttacked == false && turretBeingAttacked == false && radioBeingAttacked == false)
        {
            ZombieWalk();
        }

       if (zombieHealth<=0)
        {
            Destroy(this.gameObject);
       }


        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Barricade")
        {
            Debug.Log("collided with barricade");
            thisBarricade = col.gameObject.GetComponent<Barricade>();
            thisBarricade.zombieAIScript.Add(this);
            barricadeBeingAttacked = true;
            ZombieAttackAnimation();

        }
        if (col.gameObject.tag == "Turret")
        {
            Debug.Log("collided with turret");
            thisTurret = col.gameObject.GetComponent<Turret>();
            thisTurret.zombieAIScript.Add(this);
            turretBeingAttacked = true;
            ZombieAttackAnimation();
        }
        if (col.gameObject.tag == "Radio")
        {
            Debug.Log("collided with Radio");
            thisRadio = col.gameObject.GetComponent<Radio>();
            thisRadio.zombieAIScript.Add(this);
            radioBeingAttacked = true;
            ZombieAttackAnimation();
        }
      //  if (col.gameObject.tag == "Player")
     //   {
      //      Debug.Log("collided with Player");
      //      thisPlayer = col.gameObject.GetComponent<Movement>();
     //       thisPlayer.zombieAIScript.Add(this);
     //       PlayerBeingAttacked = true;
//ZombieAttackAnimation();
       // }

    }
        
              
    void ZombieAttackBarricade()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)

        {
            thisBarricade.barricadeHealth -= damageToBarricade;
            attackTimer = 0;
        }
    }

    void ZombieAttackTurret()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)

        {
            thisTurret.turretHealth -= damageToTurret;
            attackTimer = 0;
        }
    }

    void ZombieAttackRadio()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)

        {
            thisRadio.radioHealth -= damageToRadio;
            attackTimer = 0;
        }
    }

    void ZombieAttackAnimation()
    {
        zombieSpeed = 0;
        zombieAnimator.SetBool("attack", true);
    }

    void ZombieWalk()
    {
        zombieSpeed = 0.05f;
        zombieAnimator.SetBool("attack", false);
    }
}



