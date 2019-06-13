using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float turretHealth;
    public List<ZombieAI> zombieAIScript;
    

    public Transform target;  // represents the zombie
    public Transform head;  //the part of the turret that rotates

    public float range = 15f;
    public float turnSpeed = 5f;
    public float fireRate = 1f;
    private float fireCountDown =0f;

    public GameObject turretBulletPrefab;
    public Transform firePoint;
    

    
    void Start()
    {
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);  //calls the Update Target function and tells it to run 2 times per second, sotwice a second it will search for a new target.  
        //if you put it in the update it 
    }


    void Update()  
    {
        if (turretHealth <= 0)
        {
            Debug.Log("destroy me");
            Destroy(this.gameObject);
            for (int i = 0; i < zombieAIScript.Count; i++)
            {
               // zombieAIScript[i].turretBeingAttacked = false;
                zombieAIScript[i].attackObject = null;
            }

        }
       
        if (target == null)  // if there is no target, don't do anything
        {
            return;
        }

        // turret to turn to lock on to closest zombie
        Vector3 dir = target.position - transform.position;  // create a vector called dir that points in direction of our enemy 
        Quaternion lookRotation = Quaternion.LookRotation(dir); //calculates which way to look
        Vector3 rotation = Quaternion.Lerp(head.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;  //convert quarternion to euler angles so we can specify just the y axis to rotate around
        //using Lerp smooths out the rotation otherwise it would just jump to new target.
        head.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;  //ie if fire rate = 2 then firecountdown = 0.5 meaning it will fire a bullet every 0.5 of a second.
        }

        fireCountDown -= Time.deltaTime;  //every second, fire countdown will be reduced by 1.


    }void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(turretBulletPrefab, firePoint.position, firePoint.rotation);  //give the instantiated bullet a variable name "bulletGO" so the bullet script can access it
        TurretBullet turretBullet = bulletGo.GetComponent<TurretBullet>(); //access the turretBullet script

        if (turretBullet != null)
        {
            turretBullet.Seek(target);  //run the seek function located on the bullet script and pass to it the target variable
        }
    }
    

    void UpdateTarget()  
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");  // find targets with tag "Zombie" and store them in the Array called zombies
        float shortestDistance = Mathf.Infinity;  // make a float to store shortest distance (closest enemy). Mathf.Infinity sets it to infinity if there is no zombie 
        GameObject nearestZombie = null;  // store the nearest enemy as nearestZombie and set it to null to start with


        foreach (GameObject zombie in zombies)  
        {
            float distanceToZombie = Vector3.Distance(transform.position, zombie.transform.position);  // loop through each zombie and determine its distance and store it in variable distanceToEnemy

            if (distanceToZombie < shortestDistance)  //if you find a zombie closer than the current shortest distance value
            {
                shortestDistance = distanceToZombie;  // then set this distance to the shortest distance
                nearestZombie = zombie;  // and set this zombie in the array to the nearestZombie
            }
        }

        if (nearestZombie != null && shortestDistance <= range)  // if the Nearest zombie is not null and within our range
        {
            target = nearestZombie.transform;  //set this zombie's transform to our target 

        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()  // gives a visual of the range of the turret
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
