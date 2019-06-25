﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    public float forceMagnitude = 10, damage = 30, initialCooldown = 3, repairAmount = 5;
    float cooldown = 1;
    bool attacking, repair;
    int rotation = 1, count = 0;
    public int swingSpeed = 6;
    public string[] repairableTags;
    public GameObject wrenchSprites, wind, windSpawnPoint;
    public GameObject hitFX; // stolen from james >:D
    public GameObject sparks;

    public AudioClip smackSFX, repairSFX, repairMissSFX;
    AudioSource wrenchSFX;
    private void Start()
    {
        wrenchSFX = GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        if (Input.GetButton("Repair"))
        {
            repair = true;
        } else repair = false;
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown <= 0)
        {
            if (!attacking)
            {
                cooldown = initialCooldown;
                attacking = true;
                gameObject.GetComponent<BoxCollider>().enabled = true;

                if (repair)
                {
                    wrenchSFX.clip = repairMissSFX;
                    Camera.main.GetComponent<ScreenShake>().CamShake(.5f, .3f);
                }
                else
                {
                    wrenchSFX.clip = smackSFX;
                    Camera.main.GetComponent<ScreenShake>().CamShake(1f, .5f);
                }
                    wrenchSFX.pitch = .6f;
                wrenchSFX.Play();
                
                Instantiate(wind, windSpawnPoint.transform.position, windSpawnPoint.transform.rotation);    
            }

        }
        
    }

    private void FixedUpdate()
    {
        if (attacking)
        {
            Swipe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie" && !repair)
        {
            Debug.Log("Zombie Hit");
                other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.position - new Vector3 (transform.position.x,0,transform.position.y)* forceMagnitude, ForceMode.Impulse);
                other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                Instantiate(hitFX, other.transform.position, Quaternion.identity);
                //hitSFX.Play();
        }
        else if (repair)
        {
            Repair(other.gameObject);
        }
    }
    

    void Swipe()
    {
        
        if (rotation == 1 && count < swingSpeed)
        {
            count++;
            wrenchSprites.transform.localRotation = Quaternion.Euler(0, -60 / (swingSpeed / 2) * (swingSpeed / 2 - count), 0);
        }
        else if (count == swingSpeed && rotation == 1 && attacking)
        {
            rotation = -1;
            attacking = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            count = 0;
        }

        if (rotation == -1 && count < swingSpeed && attacking)
        {
            count++;
            wrenchSprites.transform.localRotation = Quaternion.Euler(0, 60 / (swingSpeed/2) * (swingSpeed / 2 - count), 0);
        }
        else if (count == swingSpeed && rotation == -1)
        {
            rotation = 1;
            attacking = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            count = 0;
        }


    }

    void Repair(GameObject repairable)
    {
       
        for (int i = 0; i < repairableTags.Length; i++)
        {
            if (repairable.tag == repairableTags[i])
            {
                Instantiate(sparks, repairable.transform.position + new Vector3 (0,.5f,0), Quaternion.Euler(90,0,0));
                wrenchSFX.clip = repairSFX;
                wrenchSFX.pitch = .8f;
                wrenchSFX.Play();
                if (i == 0)
                {
                    
                    if (repairable.GetComponent<ExplosiveMine>() != null)
                    {
                        repairable.GetComponent<ExplosiveMine>().isDestroyed = false;
                        repairable.GetComponentInChildren<SpriteRenderer>().sprite = repairable.GetComponent<ExplosiveMine>()._sprite;
                    }
                    else
                    {
                        if (repairable.GetComponent<BearTrap>().uses < 3)
                        {
                                repairable.GetComponent<BearTrap>().uses += 1;
                        }
                            
                        repairable.GetComponent<BearTrap>().isDestroyed = false;
                        repairable.GetComponent<BearTrap>().animator.SetBool("Destroyed", false);
                    }
                }
                else if (i == 1)
                {
                    if (repairable.GetComponent<Barricade>().barricadeHealth < 60)
                    {
                        repairable.GetComponent<Barricade>().barricadeHealth = Mathf.Clamp(repairable.GetComponent<Barricade>().barricadeHealth + repairAmount, 0, 60);
                        
                    }
                }
                else if (i == 2 || i == 3)
                {
                     if (repairable.GetComponent<Turret>().turretHealth < 100)
                    {
                        
                        if (repairable.GetComponent<Turret>().turretHealth <= 0)
                        {
                            repairable.GetComponent<Turret>().turretHealth = repairAmount;
                            //repairable.GetComponent<Turret>().ActivateTurret();
                        } else
                        repairable.GetComponent<Turret>().turretHealth = Mathf.Clamp(repairable.GetComponent<Turret>().turretHealth + repairAmount, 0, 100);
                    }
                }
            }
        }
    }
}
