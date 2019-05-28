using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    public bool isDestroyed = false;

    public float maxDamage = 50f;
    public float explosionRadius = 10f;
    public float explosionForce = 80f;

    public ParticleSystem explosionParticles;
    public Texture textureDestroyed;
    public Texture texture;
    public Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
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

                //targetHealth.zombieHealth(damage);
            }

            explosionParticles.transform.parent = null;

            explosionParticles.Play();

            Destroy(explosionParticles.gameObject, explosionParticles.main.duration);

            isDestroyed = true;

            renderer.material.SetTexture("_MainTex", textureDestroyed);

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
        renderer.material.SetTexture("_MainTex", texture);
    }
}
