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
    public bool barricadeBeingAttacked = false;
   


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

       
       
    }
    private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.name == "barricade")
            {
            Debug.Log("collided with barricade");
            zombieSpeed = 0;
                zombieAnimator.SetBool("attack", true);
            Debug.Log("barricadebeing attacked set to true");
            thisBarricade.barricadeHealth -= 10;
            }
        }
}

// if (barricadeBeingAttacked == false)
   //     {
   //         zombieSpeed = 0.05f;
   //         zombieAnimator.SetBool("attack", false);
    ///       Debug.Log("being attacked false");
    //    }









/*

void CheckBarricade()
 {
      if (thisBarricade  != null)
     {
         zombieSpeed = 0;
         zombieAnimator.SetBool("attack", true);
         thisBarricade.barricadeHealth -= 10;
     }
    else
     {
        zombieAnimator.SetBool("attack", false);
        zombieSpeed = 0.05f;
      }


 }



 void CheckPlayer()
 {
     if (Player != null)
     {
         zombieSpeed = 0;
         zombieAnimator.SetBool("attack", true);

     }
     else
     {
         zombieAnimator.SetBool("attack", false);
        zombieSpeed = 0.05f;
     }


 }
 

 private void OnTriggerEnter(Collider other)
 {
     if (other.gameObject.tag == "Wall")
     {

         transform.Rotate(Vector3.forward * rotSpeed);

     }

 }


 */


