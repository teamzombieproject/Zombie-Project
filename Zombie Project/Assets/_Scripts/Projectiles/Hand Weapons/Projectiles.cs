﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    // CODE FOR ALL HAND WEAPONS //
    //      SEE NOTES BELOW      //

    // Create different bullet prefabs for each weapon and attach this script and fill out the inspector values. //

    // Damage dealt to zombies should be in the zombies health script "TakeDamage" //

    // Zombie prefabs/models in the Hieracrchy should be tagged "Zombie" //

    public int projectileDamage = 25;       // Damge of projectile = HANGDUN/.22 RIFLE/SEMI AUTO RIFLE/HIGH POWER RIFLE//SHOTGUN >  Changee damage value to suit weapon
    public float ProjectileSpeed = 2f;      // Speed/Delay of projectile >  Best Value 10
    public float destroyProjectile = 2f;    // Time/Range before destroy is called for projectile >  Best Value 2/3
    

    private Transform myTransform;
    public GameObject bulletPrefab;         // Prefab of projectile to destroy itself on dealing damage/collision
    public Transform _BulletPrefab;         // Location of weapon bullet prefab in world for blood impact particle effects
    public GameObject bloodHitFX;           // blood on bullet impact


    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.position += Time.deltaTime * ProjectileSpeed * -myTransform.forward; // transform.up/forward/right?

        destroyProjectile -= Time.deltaTime;

        if(destroyProjectile <= 0)
        {
            Destroy(gameObject);
        }
    }
    

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.SendMessage("TakeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
            GameObject.Instantiate(bloodHitFX, _BulletPrefab.position, _BulletPrefab.rotation);
        }
    }
}


