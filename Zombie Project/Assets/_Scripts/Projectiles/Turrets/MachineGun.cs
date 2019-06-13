using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{

    /////        SEE NOTES BELOW           /////

    // Create MG bullet prefab and attach this script and fill out the inspector values. //

    // Damage dealt to zombies should be in the zombies health script "TakeDamage" //

    // Zombie prefabs/models in the Hieracrchy should be tagged "Zombie" //

    // Projectiles Prefab RB values Mass = 1, Drag = 1, Angular Drag = 1



    public int projectileDamage = 25;        // Damage > Best Value 10
    public float destroyProjectile = 2f;     // Range before destroy is called > Best Value 3/4
    public float projectileSpeed = 5f;       // Speed/Velocity > Best Value 15

    // Rigidbody Rigidbody;                  // Projectiles Prefab RB values Mass = 1, Drag = 1, Angular Drag = 1
    // public AudioSource hitSFX;
    public GameObject bloodHitFX;            // blood on bullet impact

    private Transform turretTransform;
    public GameObject bulletPrefab;          // Prefab of projectile to destroy itself on dealing damage/collision
    public Transform _MGPrefab;              // Location of machine gun bullet prefab in world for blood impact particle effects

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody = GetComponent<Rigidbody>();
        turretTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(-turretTransform.forward * projectileSpeed, ForceMode.VelocityChange); // transform.up/right/forward?
        turretTransform.position += Time.deltaTime * projectileSpeed * -turretTransform.forward;

        destroyProjectile -= Time.deltaTime;

        if (destroyProjectile <= 0)
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.SendMessage("TakeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
            GameObject.Instantiate(bloodHitFX, _MGPrefab.position, _MGPrefab.rotation);
        }

        if(collision.gameObject.tag != "Zombie") //"Untagged"
        {
            Destroy(gameObject);
        }
    }
}
