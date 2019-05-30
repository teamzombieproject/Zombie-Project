using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour

// SEE NOTES BELOW //
// If using this script for projectile fire/shooting set values below //
// Timer = 1 //
// Fire Velocity = 0.1 //

{
    public float timer = 0f, fireVelocity = 0.01f;                     // time between shots and fire speed

    public bool isShooting = false;
    public Transform bulletTransform;            // bullet spawnpoint


    public GameObject bulletPrefab;              // Projectile prefab
    Rigidbody projectile;                 // Projectiles RB

    public AudioSource fireSFX;                  // Sound effects for firing

    private void Start()
    {
        projectile = bulletPrefab.GetComponent<Rigidbody>();
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
        // GameObject.Instantiate(bulletPrefab, bulletTransform.position, bulletTransform.transform.rotation); // Bullet rotation problem here?
        Rigidbody p = Instantiate(projectile, bulletTransform.transform.position, bulletTransform.transform.rotation);
        p.velocity = transform.up * fireVelocity;
        // timer = fireVelocity;

    }
}
