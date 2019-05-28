using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDetect : MonoBehaviour
{
    // this script detects the collision of the zombie with other objects
    public GameObject currentTarget;
    ZombieAI zombieAI;

    // Start is called before the first frame update
    void Start()
    {
        zombieAI = GetComponentInParent<ZombieAI>();  //get the Zombie AI Script from parent object
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieAI.currentTarget == null)
        {
            zombieAI.currentTarget = GameObject.FindGameObjectWithTag("Radio");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            zombieAI.currentTarget = other.gameObject;  //sets the thing that the zombie has collided with to the current target

        }

        if (other.gameObject.tag == "Radio")             // if detect radio
        {
            if (zombieAI.currentTarget != null) // check to see if zombie is already colliding with something
             {
                    if (other.gameObject.tag == "Player")          // if it is colliding with something and it is the player
                     {
                    return;  // then don't change the current target, ie keep moving towards player
                     }

                    else
                     {
                     zombieAI.currentTarget = other.gameObject;  // otherwise set the current target to the Radio
                     }
             }
             else
         {
              zombieAI.currentTarget = other.gameObject; // if there is no target, then set it to the Radio
         }
       }

        if (other.gameObject.tag == "Turret")
        {
              if (zombieAI.currentTarget != null) // check to see if zombie is already colliding with something

            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Radio")  // if it is colliding with something and it is the player or the radio
                {
                    return;  // then don't change the current target, ie keep moving towards player or the radio
                }

                else
                {
                    zombieAI.currentTarget = other.gameObject;  // otherwise set the current target to the Radio
                }
                 }
                  else
                 {
                     zombieAI.currentTarget = other.gameObject; // if there is no target, then set it to the Radio
                  }
         }

     }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("exited player trigger");
           zombieAI.currentTarget = GameObject.FindGameObjectWithTag("Radio");
        }
    }


}


 