using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    public bool isDestroyed = false;

    public float maxDamage = 150f;
    public float explosionRadius = 60f;
    public float explosionForce = 0f;

    public ParticleSystem explosionParticles;
    public Sprite spriteDestroyed;
    public Sprite _sprite;
    public SpriteRenderer renderer;

    private void Start()
    {

    }

    private void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.gameObject);

        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if (targetRigidbody != null && isDestroyed == false)
        {
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            ZombieAI targetHealth = targetRigidbody.GetComponent<ZombieAI>();

            if (targetHealth != null)
            {
                float damage = CalculateDamage(targetRigidbody.position);

                targetHealth.zombieHealth -= damage;
            }

            explosionParticles.transform.parent = null;

            explosionParticles.Play();

            Destroy(explosionParticles.gameObject, explosionParticles.main.duration);

            isDestroyed = true;

            this.GetComponentInChildren<SpriteRenderer>().sprite = spriteDestroyed;

        }
        else
        {
            return;
        }



    }

    private float CalculateDamage(Vector3 targetPosition)
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

        this.GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
    }
}
