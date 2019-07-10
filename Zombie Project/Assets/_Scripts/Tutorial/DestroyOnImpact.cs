using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour
{
    int amount = 0;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name != "Rocket")
            return;
        amount++;
        if (amount >= 2)
        {
            Destroy(gameObject);
        }
    }
}
