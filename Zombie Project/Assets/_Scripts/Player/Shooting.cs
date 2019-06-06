using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour

{
    public float timer = 0.1f;                   // count down timer for fireRate
    public float fireRate = 0f;                  // Time between shots set value for each bullet

    public bool isShooting = false;
    public Transform bulletTransform;            // bullet spawn


    public GameObject bulletPrefab;              // Projectile prefab


    public AudioSource fireSFX;                  // Sound effects for firing

    private void Start()
    {
        isShooting = false;
    }

    public void Update()
    {
        CheckInput();
        timer -= Time.deltaTime;

        if (isShooting == true)
        {
            Shoot();
        }
    }

    public void CheckInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

    }

    void Shoot()
    {
        if (timer > 0)
        {
            return;
        }

        if (fireSFX != null)
        {
            fireSFX.Play();
        }

        GameObject.Instantiate(bulletPrefab, bulletTransform.position, bulletTransform.transform.rotation); 

        timer = fireRate;

    }
}
