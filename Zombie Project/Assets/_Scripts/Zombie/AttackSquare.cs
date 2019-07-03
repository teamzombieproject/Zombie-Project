using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSquare : MonoBehaviour
{
    [HideInInspector]
    public bool attacking, ended;
    public ZombieAI zombieAI;
    public BoxCollider objCollider;
    GameObject target;

    public void GiveInfo (GameObject attackTarget)
    {
        target = attackTarget;
    }
    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (ended)
        {
            objCollider.enabled = false;
            zombieAI.attackObject = null;
            transform.parent.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == zombieAI.attackObject)
        {
            zombieAI.attackObject = null;
        }
    }
    private void OnDisable()
    {
        target = null;
        transform.parent.gameObject.SetActive(false);
        objCollider.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
       // GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 0);
        if (other.gameObject == target)
        {
            if (target.tag == "Barricade")
            {
                Barricade barricade = target.GetComponent<Barricade>();
                barricade.zombieAIScript.Add(zombieAI);
                barricade.barricadeHealth -= zombieAI.damageToBarricade;
            }
            else if (target.tag == "Turret")
            {
                Turret turret = target.GetComponent<Turret>();
                turret.zombieAIScript.Add(zombieAI);
                turret.turretHealth -= zombieAI.damageToTurret;
            }

            else if (target.tag == "Radio")
            {
                Radio radio = target.GetComponent<Radio>();
                radio.zombieAIScript.Add(zombieAI);
                radio.radioHealth -= zombieAI.damageToRadio;
            }
            else if (target.tag == "Player")
            {
                target.GetComponent<Health>().TakeDamage(zombieAI.damageToPlayer, transform.position);
                zombieAI.audioSrc.Play();
            }
        }
    }
}
