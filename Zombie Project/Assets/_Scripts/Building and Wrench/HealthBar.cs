using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject following;
    public string tag;
    public Image health;
    public float distanceAbove = 80;
    Vector3 tpPoint;
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        health.gameObject.SetActive(true);
        GetComponent<Image>().enabled = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        tag = following.tag;

        if (tag == "Turret" || tag == "MachGunTurret")
        {
            health.fillAmount = following.GetComponent<Turret>().turretHealth / 100;
        }
        else if (tag == "Traps")
        {
            if (following.GetComponent<BearTrap>() != null)
            {
                float uses = following.GetComponent<BearTrap>().uses;
                health.fillAmount = uses / 3;
            }
            else
            {
                if (following.GetComponent<ExplosiveMine>().isDestroyed)
                    health.fillAmount = 0;
                else health.fillAmount = 1;
            }
        }
        else if (tag == "Barricade")
        {
            health.fillAmount = following.GetComponent<Barricade>().barricadeHealth / 60;
        }
    }
    private void LateUpdate()
    {
        if (following == null)
        {
            return;
        }
        GoToPosition();
    }

    void GoToPosition()
    {
        tpPoint = Camera.main.WorldToScreenPoint(following.transform.position) + new Vector3(0, distanceAbove);
        gameObject.transform.position = tpPoint;
        
    }
}
