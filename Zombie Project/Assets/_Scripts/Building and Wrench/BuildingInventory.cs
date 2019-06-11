using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInventory : MonoBehaviour
{
    //the buildmanager should turn on when gameManager sets it active.
    public GameManager gameManager;

    public GameObject[] buildInventory;
    public GameObject Player;

    int pickupSlot;

    private void Start()
    {
        //disable weapons and weaponswitching
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
        
        if (gameManager.State != GameManager.GameState.Build)
        {
            //deactivate everything
            //gameObject.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {

        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {

        }
        else if (Input.GetKeyUp(KeyCode.Alpha6))
        {

        }
        else if (Input.GetKeyUp(KeyCode.Alpha7))
        {

        }
        else if (Input.GetKeyUp(KeyCode.Alpha8))
        {

        }
        else if (Input.GetKeyUp(KeyCode.Alpha9))
        {

        }
    }

    void SwitchSelectedBuildable(int slot)
    {
        
        if (buildInventory[slot] == null || buildInventory[slot].GetComponent<PlaceBuildable>().quantity == 0)
        {
            //UI slot goes red for a moment
            return;
        }
        //slot sprite changes to the right sprite (using similar code as down below and a new array)
        for (int i = 0; i < buildInventory.Length; i++)
        {
            if (i == slot)
            {
                buildInventory[i].gameObject.SetActive(true);
            }
            else if (buildInventory[i] != null) buildInventory[i].gameObject.SetActive(false);
        }

    }

    public void BuildablePickup(GameObject newBuildable, int quantity)
    {
        for (int i = 0; i < buildInventory.Length; i++)
        {
            if (buildInventory[i] == newBuildable)
            {
                buildInventory[i].GetComponent<PlaceBuildable>().quantity += quantity;
                //inform HUD
                return;
            }
            else if (buildInventory[i] == null)
            {
                pickupSlot = i;
                break;
            }
        }
        buildInventory[pickupSlot] = newBuildable;
        buildInventory[pickupSlot].GetComponent<PlaceBuildable>().quantity = quantity;
        //inform HUD
    }
}
