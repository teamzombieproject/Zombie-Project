using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] WeaponInventory;
    int selected;
    public Transform gunSpawn;
    public AudioClip switchGunSFX, pickupGunSFX;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SwitchWeapons(0);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SwitchWeapons(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SwitchWeapons(2);
        }
    }

    void SwitchWeapons(int number)
    {
        //check if the weapon being switched to is held or doesnt exist
        if (selected == number || WeaponInventory[number] == null)
            return;

        //disable all deselected guns and enable selected one
        for (int i = 0; i < WeaponInventory.Length; i++)
        {
            if (i == number)
            {
                WeaponInventory[i].gameObject.SetActive(true);
            }
            else if (WeaponInventory[i] != null) WeaponInventory[i].gameObject.SetActive(false);
        }

        //make selected equal the new selected guns number for use with gun pickup
        selected = number;

        audioSource.clip = switchGunSFX;
        audioSource.Play();

        //GameObject[] gun = GameObject.FindGameObjectsWithTag("SelectedGun");
        //for (int i = 0; i < gun.Length; i++)
        //{
        //    Destroy(gun[i]);
        //}
        //Instantiate(WeaponInventory[number], gunSpawn);
        //selected = number;
        //WeaponInventory[selected] = GameObject.FindGameObjectWithTag("SelectedGun");
        
    }

    public void WeaponPickup(string newWeapon, Transform spawnTransform)
    {
        if (selected == 2)
        {
            SwitchWeapons(0);
        }
        audioSource.clip = pickupGunSFX;
        audioSource.Play();
        Destroy(spawnTransform.gameObject);
        if (WeaponInventory[1] == null)
        {
            WeaponInventory[1] = gunSpawn.transform.Find(newWeapon).gameObject;
            SwitchWeapons(1);
            audioSource.clip = pickupGunSFX;
            audioSource.Play();
            return;
        }

        

        
        Instantiate(WeaponInventory[selected].GetComponent<DropInfo>().DropVersion, spawnTransform.position, Quaternion.Euler(0,Random.Range(0,360),0));
        
    }
}
