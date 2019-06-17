using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBuildable : MonoBehaviour
{
    public string placeableBuildableName;
    public int quantity = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("BuildManager").GetComponent<BuildingInventory>().BuildablePickup(GameObject.FindGameObjectWithTag("BuildManager").transform.Find(placeableBuildableName).gameObject, quantity);
            //Destroy(gameObject);
        }
    }
}
