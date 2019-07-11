﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTurret : MonoBehaviour
{
    public float turretHealth;
    public List<ZombieAI> zombieAIScript;


    public Transform target;  // represents the zombie
    public Transform head;  //the part of the turret that rotates

    public float range = 15f;
    public float turnSpeed = 5f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    float delayCountDown;
    bool turretActive = true;
    public GameObject turretBulletPrefab;
    public Transform[] firePoint;
    public Material activeMat;
    public Material deadMat;
    public ParticleSystem smokeParticle;

    AudioSource audioSrc;
    public AudioClip audioClip;

    public Renderer headRenderer;
    public Renderer bodyRenderer;
    public bool smokeParticlePlaying = false;

    float timer = 0;
    public GameObject wall;


    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);  //calls the Update Target function and tells it to run 2 times per second, sotwice a second it will search for a new target.  
        //if you put it in the update it 

        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = audioClip;
        smokeParticle.Clear();


    }

    void Update()
    {
        if (turretActive = true && turretHealth > 0)
        {
            ActivateTurret();
        }
        if (turretHealth <= 0)
        {

            DeactivateTurret();

            for (int i = 0; i < zombieAIScript.Count; i++)
            {
                zombieAIScript[i].attackObject = null;
            }

        }

        if (target == null)  // if there is no target, don't do anything
        {
            return;
        }

        if (turretHealth > 0)
        {
            // turret to turn to lock on to closest zombie
            Vector3 dir = target.position - transform.position;  // create a vector called dir that points in direction of our enemy 
            Quaternion lookRotation = Quaternion.LookRotation(dir); //calculates which way to look
            Vector3 rotation = Quaternion.Lerp(head.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;  //convert quarternion to euler angles so we can specify just the y axis to rotate around
                                                                                                                      //using Lerp smooths out the rotation otherwise it would just jump to new target.
            head.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            timer += Time.deltaTime;
        }
        if (timer > 4.5)
        {
            Destroy(wall);
            target = null;
        }
        if (fireCountDown <= 0 && turretActive == true)
        {
            Shoot();
            fireCountDown = fireRate;
            delayCountDown = fireCountDown / 2;
        }

        if (firePoint.Length > 1 && delayCountDown <= 0 && turretActive == true)
        {
            Shoot2();
            delayCountDown = fireRate;
        }

        fireCountDown -= Time.deltaTime;  //every second, fire countdown will be reduced by 1.
        delayCountDown -= Time.deltaTime;


    }
    void Shoot()
    {
        GameObject bulletGo = Instantiate(turretBulletPrefab, firePoint[0].position, firePoint[0].rotation);  //give the instantiated bullet a variable name "bulletGO" so the bullet script can access it
        audioSrc.Play();
    }

    void Shoot2()
    {
        GameObject bulletGo = Instantiate(turretBulletPrefab, firePoint[1].position, firePoint[1].rotation);
        audioSrc.Play();
    }


    void UpdateTarget()
    {
        
    }

    void DeactivateTurret()
    {
        turretHealth = 0;
        turretActive = false;
        if (smokeParticlePlaying == false)
        {
            smokeParticle.Play();
            smokeParticlePlaying = true;
        }

        bodyRenderer.material = deadMat;
        headRenderer.material = deadMat;

    }

    public void ActivateTurret()
    {
        turretActive = true;


        bodyRenderer.material = activeMat;
        headRenderer.material = activeMat;
        smokeParticle.Stop();
        smokeParticlePlaying = false;

    }

    private void OnDrawGizmosSelected()  // gives a visual of the range of the turret
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
