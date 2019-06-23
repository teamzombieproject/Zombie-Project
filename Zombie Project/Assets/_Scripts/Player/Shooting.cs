using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour


    // ALL VALUES SET ON EACH GUN PREFAB TO MATCH ZOMBIE PROJECT GAME DESIGN DOCUMENT //    


{
    public float timer = 0.1f;                   // count down timer for fireRate
    public float fireRate = 0f;                  // Time between shots set value for each bullet
    public float reloadTime = 2f;                // Time to reload
    public float curReloadTime;                  // current time in reload
    public float ammoCount;                      // Count of bullets in gun 
    public float timeBetweenShots = 0.1f;        // Time between bullets

    public int curAmmo;                          // Current ammo of the weapon
    public int maxAmmo = 30;                     // Starting ammo of the weapon
    public int ammoReload;                       // Reload amount
    public int fireMode;                         // Full Auto = 1, Single Shot = 2, Burst fire = 3

    public bool isReloading;                     // Player has pushed R
    public bool isShooting = true;               // Player has pushed Fire1
    public bool isShotGun = false;               // Player is using shotgun
    //public bool isSemiAuto = false;            // Done in Fire Mode
    //public bool isFullAuto = false;            // Done in Fire Mode

    public GameObject reloadReminder;            // reload text object "reload" reminder for player
    public Transform[] bulletTransform;          // bullet spawn 
    public GameObject bulletPrefab;              // Projectile prefab
    public AudioSource fireSFX;                  // Sound effects for firing
    //public AudioSource reloadSFX;              // Reload sound efffect

    public void Start()
    {
        ammoReload = 2147483647;                 // MaxValue for Integer
    }
  
    private void Update()
    {
        timer -= Time.deltaTime;

        if (fireMode == 1) // FullAuto = High Power Rifle
        {
            if (Input.GetButton("Fire1") && !isReloading && curAmmo > 0)
            {
                if (isShooting)
                {
                    curAmmo--;
                    isShooting = false;
                    Shoot();
                    StartCoroutine(ShootingPewPew());
                }
            }
        }
        else if (fireMode == 2) // Single Fire = Handgun, .22 Rifle and Shotgun
        {
            if (Input.GetButtonDown("Fire1") && !isReloading && curAmmo > 0)
            {
                if (isShooting)
                {
                    curAmmo--;
                    isShooting = false;
                    Shoot();
                    StartCoroutine(ShootingPewPew());
                }
            }
        }
        else if (fireMode == 3) // Burst Fire = Semi Auto Rifle
        {
            if (Input.GetButtonDown("Fire1") && !isReloading && curAmmo > 0 && ammoCount == 0)
            {
                isShooting = true;
                BurstFire();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammoReload > 0)
        {
            curReloadTime = reloadTime;
            isReloading = true;
            Reload();
        }

        if (isReloading)
        {
            curReloadTime -= Time.fixedDeltaTime;

            if (curReloadTime <= 0)
            {
                isReloading = false;
            }
        }

        if (ammoCount == 3)
        {
            ammoCount = 0;
        }

        if (curAmmo == 0)                                   // Not working on prefab only works when game object reloadReminder is dragged onto the prefab copy in the hierachy
        {
         reloadReminder.gameObject.SetActive(true);
        }
        else
        {
         reloadReminder.gameObject.SetActive(false);
        }
    }

    void BurstFire()
    {
        if (ammoCount < 3)
        {
            if (curAmmo > 0)
            {
                curAmmo--;
                Shoot();
                StartCoroutine(ShootingPewPew());
            }
        }
    }

    void Reload()
    {
        var shot = maxAmmo - curAmmo;
        ammoCount = 0;

        if (ammoReload < shot)
        {
            curAmmo += ammoReload;
            ammoReload = 0;
        }
        else
        {
            curAmmo += shot;
            ammoReload -= shot;
        }
    }

    IEnumerator ShootingPewPew()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        if (fireMode != 3)
        {
            isShooting = true;
        }

        if (fireMode == 3)
        {
            ammoCount++;
            BurstFire();
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

        Instantiate(bulletPrefab, bulletTransform[0].position, bulletTransform[0].transform.rotation);

        if (isShotGun)
        {
            Instantiate(bulletPrefab, bulletTransform[1].position, bulletTransform[1].transform.rotation);
            Instantiate(bulletPrefab, bulletTransform[2].position, bulletTransform[2].transform.rotation);
        }

        timer = fireRate;
    }
}
