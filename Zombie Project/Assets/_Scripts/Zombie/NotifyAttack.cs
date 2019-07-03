using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyAttack : MonoBehaviour
{
    public void Notify(string whatToNotify)
    {
        if (whatToNotify == "Attack")
        {
            GetComponentInParent<ZombieAI>().navAgent.isStopped = true;
            GetComponentInParent<ZombieAI>().attack = true;
        }
        if (whatToNotify == "End")
        {
            //GetComponentInParent<ZombieAI>().CARIAWDHAWIUDBAWD = true;
            GetComponentInParent<ZombieAI>().navAgent.isStopped = false;
            GetComponentInParent<ZombieAI>().ended = true;
            //transform.parent.Find("Pivot").GetComponentInChildren<AttackSquare>().ended = true;
        }
    }
}
