using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCrates : MonoBehaviour
{
    public bool supplyCrate = false;
    public ParticleSystem pickupFX;
    public GameObject mineDrop, turretDrop, buildInventory;
    public PlayerRadio PlayerRadio;
    private void Start()
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && supplyCrate)
        {
            Instantiate(mineDrop, gameObject.transform.position, Quaternion.identity);
            Instantiate(turretDrop, gameObject.transform.position, Quaternion.identity);
            Instantiate(pickupFX, transform.position + new Vector3(2.5f, 0, 2.5f), Quaternion.Euler(-45, 0, 0));
            buildInventory.GetComponent<BuildingInventory>().forceOn = true;
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Player" && !supplyCrate)
        {
            Instantiate(pickupFX, transform.position + new Vector3(2.5f, 0, 2.5f), Quaternion.Euler(-45, 0, 0));
            PlayerRadio.pickedUp++;
            Destroy(gameObject);
            
        }
    }

    
}
