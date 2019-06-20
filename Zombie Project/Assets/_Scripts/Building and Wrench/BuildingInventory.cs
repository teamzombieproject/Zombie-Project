using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInventory : MonoBehaviour
{
    //the buildmanager should turn on when gameManager sets it active.
    public GameManager gameManager;
    public GameObject[] buildInventory;
    public GameObject[] boxes; // selection boxes
    //public Sprite[] Icons; //bear trap, mine, turret, machgunturret, barricade
    GameObject Player, Arm;
    public bool on = false;
    int pickupSlot;

    private void Start()
    {
        //disable weapons and weaponswitching
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if (gameManager.State == GameManager.GameState.Build)
        {
            //tempOn = false;
            on = true;
            Player.transform.Find("Arm").GetComponent<Aiming>().enabled = false;
            Player.transform.Find("Arm").GetComponent<AudioSource>().enabled = false;
            Player.transform.Find("Arm").GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("InventorySelections").gameObject.SetActive(true);
        }
        else
        {
           // tempOff = false;
            on = false;
            Disable();
        }
        // if (gameManager.State == GameManager.GameState.Build)
        // {
        //     on = true;
        //     Player.transform.Find("Arm").GetComponent<Aiming>().enabled = false;
        //     Player.transform.Find("Arm").GetChild(0).gameObject.SetActive(false);
        // } else
        // {
        //     on = false;
        //     Player.transform.Find("Arm").GetComponent<Aiming>().enabled = true;
        //     Player.transform.Find("Arm").GetChild(0).gameObject.SetActive(true);
        // }
        if (on)
        {
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
    }

    void SwitchSelectedBuildable(int slot)
    {

        //slot sprite changes to the selected (using similar code as changing what objects are active down below and a new array)
        boxes[0].transform.parent.GetComponent<AudioSource>().Play();
        for (int i = 0; i < boxes.Length; i++)
        {
            if (i == slot)
            {
                boxes[i].GetComponent<Outline>().enabled = true;
            }
            else
                boxes[i].GetComponent<Outline>().enabled = false;
        }
        if (buildInventory[slot] == null || buildInventory[slot].GetComponent<PlaceBuildable>().quantity == 0)
        {
            slot = -1; //make it so none of the items get set active
        }

        for (int i = 0; i < buildInventory.Length; i++)
        {
            if (i == slot)
            {
                buildInventory[i].gameObject.SetActive(true);
            }
            else if (buildInventory[i] != null)
            {
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
                quantity += buildInventory[i].GetComponent<PlaceBuildable>().quantity;
                boxes[i].GetComponentInChildren<Text>().text = quantity.ToString();
                buildInventory[i].GetComponent<PlaceBuildable>().quantity = quantity;
                
                return;
            }
            else if (buildInventory[i] == null && !(pickupSlot < i))
            {
                pickupSlot = i;
                break;
            }
        }
        buildInventory[pickupSlot] = newBuildable;
        buildInventory[pickupSlot].GetComponent<PlaceBuildable>().quantity = quantity;
        boxes[pickupSlot].GetComponentInChildren<Text>().text = quantity.ToString();
        boxes[pickupSlot].transform.Find("PickupIcon").gameObject.GetComponent<Image>().enabled = true;
        boxes[pickupSlot].transform.Find("PickupIcon").gameObject.GetComponent<Image>().sprite = newBuildable.GetComponent<IconInfo>().Icon;
        if (boxes[pickupSlot].GetComponent<Outline>().enabled == true)
        {
            buildInventory[pickupSlot].gameObject.SetActive(true);
        }
        pickupSlot = 99;
    }

    private void Disable()
    {
        //deactivate all UI
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].GetComponent<Outline>().enabled = false;
        }
        for (int i = 0; i < buildInventory.Length; i++)
        {
            if (buildInventory[i] != null)
            {
                buildInventory[i].SetActive(false);
            }
        }
        //GameObject.FindGameObjectWithTag("BuildInventory").SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.Find("Arm").GetComponent<Aiming>().enabled = true;
        Player.transform.Find("Arm").GetChild(0).gameObject.SetActive(true);
        Player.transform.Find("Arm").GetComponent<AudioSource>().enabled = true;
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("InventorySelections").gameObject.SetActive(false);
    }
}
