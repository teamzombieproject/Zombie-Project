﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{

    /////        SEE NOTES BELOW           /////

    // Create MG bullet prefab and attach this script and fill out the inspector values. //

    // Damage dealt to zombies should be in the zombies health script "TakeDamage" //

    // Zombie prefabs/models in the Hieracrchy should be tagged "Zombie" //



    public int projectileDamage = 25;        // Damage
    public float destroyProjectile = 2f;     // Range before destroy is called
    public float speed = 5f;                 // Speed/Velocity

    Rigidbody m_Rigidbody;
    public AudioSource hitSFX;

    private Transform turretTransform;

    // Start is called before the first frame update
    void Start()
    {
        //m_Rigidbody = GetComponent<Rigidbody>();
        turretTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(turretTransform.up * speed, ForceMode.VelocityChange); // transform.up?

        destroyProjectile -= Time.deltaTime;

        if (destroyProjectile <= 0)
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.SendMessage("TakeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);

            hitSFX.Play();

        }
    }
}
