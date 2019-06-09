using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupWeapon : MonoBehaviour
{
    WeaponSwitching weaponSwitching;
    Image circle;
    public GameObject outline;
    bool fill, eIsHeld;
    public GameObject weaponDropped;
    public float speed = .3f;
    public string GunName;
    
    

    private void Start()
    {
        weaponSwitching = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitching>();
        circle = GameObject.FindGameObjectWithTag("Player").transform.Find("Canvas").GetComponentInChildren<Image>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player entered weapon drop collision");
            if (weaponSwitching == null)
            {
                weaponSwitching = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitching>();
                if (weaponSwitching == null)
                    return;
            }
            
            fill = true;
            circle.fillAmount = 0;
            outline.SetActive(true);
            
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            eIsHeld = true;
        }
        else eIsHeld = false;
        if (fill)
        {
            if (eIsHeld)
            {
                circle.fillAmount += Time.deltaTime * speed;
                if (circle.fillAmount == 1)
                {
                    fill = false;
                    circle.fillAmount = 0;
                    weaponSwitching.WeaponPickup(GunName, gameObject.transform);
                    circle.fillAmount = 0;
                    outline.SetActive(false);
                    Destroy(gameObject);
                }
            }
            else if (circle.fillAmount > 0)
            {
                circle.fillAmount -= .05f;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if  (other.tag == "Player")
        circle.fillAmount = 0;
        fill = false;
        outline.SetActive(false);
    }
}
