using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour

    //  SEE NOTES BELOW //

    // Inspector values  //

    // Rocket Damage = 100 direct hit kills zombie //
    // Splash Damage = Set value for splash damage currently set to 10 in the inspector //
    // Rocket Speed = Currently set to 2 in the inspector //
    // Rocket Range = Currently set to 2 in the inspector //

{
    public float rocketDamage = 100f;   // Damage to enemy for direct hit
    public float splashDamage = 0f;     // Splash Damage = neabry zombies in radius of explosion will recieve splash damage amount/value
    public float rocketSpeed = 5f;      // Speed of rocket
    public float rocketRange = 10f;     // Range of rocket before destroy is called


    public GameObject explosionEffect;  // Eplosion particle effect
    public GameObject bulletPrefab;     // Prefab of projectile to destroy itself on dealing damage/collision to zombie
    public Transform rocketPrefab;      // Rocket prefab position in world for explosion to Instatiate 
    // Rigidbody m_Rigidbody;              // Rockets Prefab RB values Mass = 1, Drag = 1, Angular Drag = 1

    private Transform turretTransform;  // Rocket Transform/spawn = player gun
    

    // Start is called before the first frame update
    void Start()
    {
        // m_Rigidbody = GetComponent<Rigidbody>();
        turretTransform = transform;


    }

    // Update is called once per frame
    void Update()
    {
        // gameObject.GetComponent<Rigidbody>().AddForce(-turretTransform.forward * rocketSpeed, ForceMode.VelocityChange); // transform.up/right/forward?
        turretTransform.position += Time.deltaTime * rocketSpeed * turretTransform.forward;
        rocketRange -= Time.deltaTime;

        if (rocketRange <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
      
        {
            ZombieAI zomb = collision.gameObject.GetComponent<ZombieAI>();

            zomb.gameObject.SendMessage("TakeDamage", rocketDamage, SendMessageOptions.DontRequireReceiver);
            zomb.isHit = true;

            //  collision.gameObject.SendMessage("TakeDamage", rocketDamage, SendMessageOptions.DontRequireReceiver);
            //  Destroy(gameObject);

            GameObject.Instantiate(explosionEffect, rocketPrefab.position, rocketPrefab.rotation);

            if (splashDamage > 0f)
            {
                DamageRadius();
            }

        }
        if (collision.gameObject.tag != "Zombie")
        {
            Destroy(gameObject);
        }
    }

    void DamageRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, splashDamage);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Zombie")
            {
                collider.gameObject.SendMessage("TakeDamage", splashDamage, SendMessageOptions.DontRequireReceiver);                
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashDamage);  // See radius on rocket prefab for splash damage
    }
}
