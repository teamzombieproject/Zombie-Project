using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{ 
    public GameObject Player;
    public GameObject Zombie;
    public GameObject Turret;
    public float zombieSpeed;
    bool gotoTurret = true;
    bool gotoPlayer = false;
    public int zombieHealth;
    public GameObject barricade;
    public float rotSpeed;
    

    void Start()
    {
     barricade.GetComponent<Barricade>();
       
    }
    

    // Update is called once per frame
    void Update()
    {
        if (gotoTurret == true)
        {
            transform.LookAt(Turret.transform);
            transform.position = Vector3.MoveTowards(transform.position, Turret.transform.position, zombieSpeed);
        }
        if (gotoPlayer == true)
        {
            transform.LookAt(Player.transform);
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, zombieSpeed);
        }
           
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
         
        {
            gotoTurret = false;
            gotoPlayer = true;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        gotoTurret = true;
        gotoPlayer = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barricade")
        {
            zombieSpeed = 0;
            
            barricade.GetComponent<Barricade>().barricadeHealth -= 10;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
       zombieSpeed = 0.05f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            
            transform.Rotate(Vector3.forward * rotSpeed);
           
        }

    }





}