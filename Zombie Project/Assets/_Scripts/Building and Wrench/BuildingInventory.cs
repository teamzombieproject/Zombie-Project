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
        Player.transform.Find("Arm").gameObject.SetActive(false);
    }
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.Find("Arm").gameObject.SetActive(false);
        }

        //if (gameManager.State != GameManager.GameState.Build)
        //{
            //deactivate all UI
            //Player.transform.Find("Arm").gameObject.SetActive(true);
            //for (int i = 0; i < buildInventory.Length; i++)
            //{ 
                //if(buildInventory[i] != null)
                //{
                    //buildInventory[i].gameObject.transform.position = new Vector3(999, 999, 999);
                    //buildInventory[i].GetComponent<PlaceBuildable>().Reset(0, new Color(0, 1, 0, .5f));
                    //buildInventory[i].setActive = false;
                //}
            //}
            //gameObject.SetActive(false);
        //}

            if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SwitchSelectedBuildable(0);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SwitchSelectedBuildable(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SwitchSelectedBuildable(2);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SwitchSelectedBuildable(3);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SwitchSelectedBuildable(4);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SwitchSelectedBuildable(5);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            SwitchSelectedBuildable(6);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            SwitchSelectedBuildable(7);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            SwitchSelectedBuildable(8);
        }

    }

    void SwitchSelectedBuildable(int slot)
    {

        //slot sprite changes to the selected (using similar code as changing what objects are active down below and a new array)

        if (buildInventory[slot] == null || buildInventory[slot].GetComponent<PlaceBuildable>().quantity == 0)
        {
            return;
        }

        for (int i = 0; i < buildInventory.Length; i++)
        {
            if (i == slot)
            {
                buildInventory[i].gameObject.SetActive(true);
            }
            else if (buildInventory[i] != null)
            {
                buildInventory[i].gameObject.transform.position = new Vector3(999, 999, 999);
                buildInventory[i].GetComponent<PlaceBuildable>().Reset(0, new Color(0, 1, 0, .5f));
                buildInventory[i].gameObject.SetActive(false);
            }
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
