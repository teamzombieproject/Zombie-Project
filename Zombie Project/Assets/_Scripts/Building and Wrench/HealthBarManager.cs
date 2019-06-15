using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public GameObject healthBar;
    GameObject nHB;
    GameObject[] traps, turret, machGunTurret, barricade, healthBars; //was going to be really fancy and have all tags add themselves automatically but decided the work wasn't worth it.
    int amount;
    List<GameObject> healthBarList = new List<GameObject>(); // apparently its easier to add stuff to lists

    
    private void Start()
    {
        traps = GameObject.FindGameObjectsWithTag("Traps");
        turret = GameObject.FindGameObjectsWithTag("Turret");
        machGunTurret = GameObject.FindGameObjectsWithTag("MachGunTurret");
        barricade = GameObject.FindGameObjectsWithTag("Barricade");

        amount = traps.Length + turret.Length + machGunTurret.Length + barricade.Length;
        for (int i = 0; i < amount; i++)
        {
            nHB = Instantiate(healthBar, GameObject.FindGameObjectWithTag("Canvas").transform);
            if (i < traps.Length)
            {
                nHB.GetComponent<HealthBar>().following = traps[i];
            }
            else if (i - traps.Length < turret.Length)
            {
                nHB.GetComponent<HealthBar>().following = turret[i - traps.Length];
            }
            else if (i - (turret.Length + traps.Length) < machGunTurret.Length)
            {
                nHB.GetComponent<HealthBar>().following = machGunTurret[i - (turret.Length + traps.Length)];
            }
            else if (i - (turret.Length + machGunTurret.Length + traps.Length) < barricade.Length)
            {
                nHB.GetComponent<HealthBar>().following = barricade[i - (turret.Length + machGunTurret.Length + traps.Length)];
            }
        }
        healthBars = GameObject.FindGameObjectsWithTag("HealthBar");
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (healthBars[i] != null)
            healthBarList.Add(healthBars[i]);
            Debug.Log("adding;");
        }
    }
    //    healthBars = GameObject.FindGameObjectsWithTag("HealthBar");

    //    for (int i = 0; i < traps.Length; i++)
    //    {
    //        healthBars[i].GetComponent<HealthBar>().following = traps[i];
    //        healthBars[i].GetComponent<HealthBar>().tag = "Traps";
    //        Debug.Log("trap");
    //    }
        
    //    for (int i = traps.Length; i < turret.Length; i++)
    //    {
    //        healthBars[i].GetComponent<HealthBar>().following = turret[i];
    //        healthBars[i].GetComponent<HealthBar>().tag = "Turret";
    //        Debug.Log("turr");
    //    }
    //    for (int i = turret.Length; i < machGunTurret.Length; i++)
    //    {
    //        healthBars[i].GetComponent<HealthBar>().following = machGunTurret[i];
    //        healthBars[i].GetComponent<HealthBar>().tag = "machGunTurret";
    //        Debug.Log("machgunturr");
    //    }
    //    for (int i = 0; i < barricade.Length; i++)
    //    {
    //        healthBars[i].GetComponent<HealthBar>().following = barricade[i];
    //        healthBars[i].GetComponent<HealthBar>().tag = "Barricade";
    //        Debug.Log("barr");
    //    }
        

    public void OnDisable()
    {
        for(int i = 0; i < healthBarList.Count; i++)
        {
            if (healthBarList[i] != null)
                Destroy(healthBarList[i]);
        }
    }


    public void AddHealthBar(GameObject buildable)
    {
        GameObject newHealthBar = Instantiate(healthBar, GameObject.FindGameObjectWithTag("Canvas").transform);
        Debug.Log("object created");
        newHealthBar.GetComponent<HealthBar>().following = buildable;
        healthBarList.Add(newHealthBar);
        newHealthBar.transform.position = new Vector3(999, 999);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Repair"))
        {
            Debug.Log("HealthBars are visible");
            for (int i = 0; i < healthBarList.Count; i++)
            {
                if (healthBarList[i] != null)
                {
                    healthBarList[i].SetActive(true);
                }
            }
        }
        if (Input.GetButtonUp("Repair"))
        {
            for (int i = 0; i < healthBarList.Count; i++)
            {
                if (healthBarList[i] != null)
                {
                    healthBarList[i].SetActive(false);
                }
            }
        }
    }
}
