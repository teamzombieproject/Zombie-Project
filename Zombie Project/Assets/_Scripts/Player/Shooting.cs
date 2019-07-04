using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour


    // ALL VALUES SET ON EACH GUN PREFAB TO MATCH ZOMBIE PROJECT GAME DESIGN DOCUMENT //    


{
    public float reloadTime = 2f;                // Time to reload
    public float curReloadTime;                  // current time in reload
    public float ammoCount;                      // Count of bullets in gun 
    public float timeBetweenShots = 0.1f;        // Time between bullets


    public int curAmmo;                          // Current ammo of the weapon
    public int maxAmmo = 30;                     // Starting ammo of the weapon
    public int ammoReload;                       // Reload amount
    public int fireMode;                         // Full Auto = 1, Single Shot = 2, Burst fire = 3

    public bool isReloading;                     // Player has pushed R
    public bool isShooting = false;              // Player has pushed Fire1
    public bool isShotGun = true;                // Player is using shotgun

    public string weaponName = "";

    public GameObject ammoText;                  // Ammo text counter on HUD 
    public GameObject weaponText;                // Weapon text on HUD
    public Transform[] bulletTransform;          // bullet spawn for projectile and fireFX
    public GameObject bulletPrefab;              // Projectile prefab
    public AudioSource fireSFX;                  // Sound effects for firing
    public GameObject fireFx;                    // Particle effect when firing guns - (Shotgun has its own particle effect)
    public GameObject shellPrefab;
    public GameObject shellSocket;

    public AudioSource reloadSFX;                // Reload sound efffect Final thing to do

    GameObject gameManager;


    public void Start()
    {
        ammoReload = 2147483647;                 // MaxValue for Integer

        gameManager = GameObject.FindGameObjectWithTag("GameController");
        ammoText = GameObject.Find("Bullet Count");
        weaponText = GameObject.Find("Weapon Name");
    }

    public void OnEnable()
    {
        isShooting = false;
    }
  
    private void Update()
    {
        weaponText.GetComponent<Text>().text = weaponName.ToString();  // Display Weapon name player is using on hud
        ammoText.GetComponent<Text>().text = curAmmo.ToString();       // Display ammo count before getbutton

        if (Input.GetButtonDown("Fire1"))
        {
            ammoText.GetComponent<Text>().text = curAmmo.ToString();  // Display Ammo count when button down to update count
        }


        if (fireMode == 1 || fireMode == 2) // Fire mode 1 = FullAuto(High Power Rifle) Fire mode 2 = Single fire(Desert Eagle, Handgun, .22 & Shotgun)
        {
            if (Input.GetButton("Fire1") && !isReloading && curAmmo > 0)
            {
                if (!isShooting) //Check not shooting already
                {
                    curAmmo--;
                    isShooting = true;  //Set shoot flag so we cant shoot
                    Shoot();
                    StartCoroutine(ShootingPewPew());

                    Camera.main.GetComponent<ScreenShake>().CamShake(.7f, .2f); //Camera shake when firing guns
                }
            }
        }
        else if (fireMode == 3) //Burst Fire = Semi Auto Rifle
        {
            if (Input.GetButtonDown("Fire1") && !isReloading && curAmmo > 0 && ammoCount == 0)
            {
                isShooting = true;
                BurstFire();

                Camera.main.GetComponent<ScreenShake>().CamShake(.8f, .2f); //Camera shake when firing Semi Auto
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammoReload > 0)
        {
            reloadSFX.Play();
            curReloadTime = reloadTime;
            isReloading = true;
            Reload();
        }

        if (isReloading)
        {
            curReloadTime -= Time.deltaTime;

            if (curReloadTime <= 0)
            {
                isReloading = false;
            }

        }

        if (ammoCount == 3)
        {
            ammoCount = 0;
        }

        if (curAmmo == 0)                                   
        {
            gameManager.GetComponent<GameManager>().reloadGUIObject.SetActive(true);
        }
        else
        {
            gameManager.GetComponent<GameManager>().reloadGUIObject.SetActive(false);
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
            isShooting = false; //Reset shot flag so we can shoot again
        }

        if (fireMode == 3)
        {
            ammoCount++;
            BurstFire();
        }
    }

    void Shoot()
    {
        if (fireSFX != null)
        {
            fireSFX.Play();
            Instantiate(fireFx, bulletTransform[0].position, bulletTransform[0].rotation);
        }

         Instantiate(bulletPrefab, bulletTransform[0].position, bulletTransform[0].transform.rotation);
        Instantiate(shellPrefab, shellSocket.transform.position, new Quaternion(0,0,0,0));
         //Instantiate(fireFx, bulletTransform[0].position, bulletTransform[0].rotation);

        if (isShotGun)
        {
            Instantiate(bulletPrefab, bulletTransform[1].position, bulletTransform[1].transform.rotation);
            Instantiate(bulletPrefab, bulletTransform[2].position, bulletTransform[2].transform.rotation);

        }
    }
}

