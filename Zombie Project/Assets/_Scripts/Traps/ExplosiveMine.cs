using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    public bool isDestroyed = false;

    public float maxDamage = 50f;
    public float explosionRadius = 10f;
    public float explosionForce = 80f;

    private void OnTriggerEnter(Collider other)
    {

        if (isDestroyed == false)
        {
            // Activate explosion
            // Deal damage
            // Set sprite to destroyed
            isDestroyed = true;
        }
        else
        {
            return;
        }
    }

    private float CalculateDamage(Vector3 targetPosition)       // From the tank game to try and calculate the damge due to how close they are (might need to be recalculated for this purpose)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        float damage = relativeDistance * maxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }

    private void Repaired()
    {
        isDestroyed = false;
        // Change sprite back to normal
    }
}
