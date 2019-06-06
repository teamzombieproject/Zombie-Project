using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] WeaponInventory;
    public GameObject[] Droppable;
    int selected;
    public Transform gunSpawn;
    
    private void Start()
    {
        
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
        if (selected == number)
            return;
        Destroy(GameObject.FindGameObjectWithTag("SelectedGun"));
        Instantiate(WeaponInventory[number], gunSpawn);
        selected = number;
    }

    public void WeaponPickup(GameObject newWeapon, GameObject newWeaponDroppable, Transform spawnTransform)
    {
        if (selected == 2)
        {
            SwitchWeapons(0);
        }
        Instantiate(Droppable[selected], spawnTransform.position, Quaternion.Euler(0,Random.Range(0,360),0));
        Destroy(GameObject.FindGameObjectWithTag("SelectedGun"));
        WeaponInventory[selected] = newWeapon;
        Droppable[selected] = newWeaponDroppable;
        Instantiate(WeaponInventory[selected], gunSpawn);
        
    }
}
