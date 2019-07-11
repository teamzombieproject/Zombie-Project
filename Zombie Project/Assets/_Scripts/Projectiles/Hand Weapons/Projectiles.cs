using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    // CODE FOR ALL HAND WEAPONS //
    //      SEE NOTES BELOW      //

    // Damage dealt to zombies should be in the zombies health script "TakeDamage" //

    // Zombie prefabs/models in the Hieracrchy should be tagged "Zombie" //

    public int projectileDamage = 25;       // Damge of projectile = HANGDUN/.22 RIFLE/SEMI AUTO RIFLE/HIGH POWER RIFLE//SHOTGUN >  Changee damage value to suit weapon
    public float projectileSpeed = 2f;      // Speed/Delay of projectile >  Best Value 10
    public float destroyProjectile = 2f;    // Time/Range before destroy is called for projectile >  Best Value 2/3
    public float kickBack = 10f;            // Zombie staggers back
    public bool isDeagle;                   // If Desert Eagle is checked (most powerfull gun in game has long reload time and ammo count only 4) bullet goes through enemies
    

    private Transform myTransform;
    public GameObject bulletPrefab;         // Prefab of projectile to destroy itself on dealing damage/collision
    public Transform _BulletPrefab;         // Location of weapon bullet prefab in world for blood impact particle effects
    public GameObject bloodHitFX;           // blood on bullet impact + sound effect
 

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.position += Time.deltaTime * projectileSpeed * myTransform.forward; // transform.up/forward/right?

        destroyProjectile -= Time.deltaTime;

        if (destroyProjectile <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            ZombieAI zomb = collision.gameObject.GetComponent<ZombieAI>();
            
            //zomb.gameObject.SendMessage("TakeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);
            zomb.gameObject.GetComponent<ZombieHealth>().Hurt(projectileDamage, transform.position, 0.5f);
            zomb.isHit = true;

            Vector3 shotDir = zomb.transform.position - transform.position;
            shotDir.y = 0;

            zomb.rb.AddForce(shotDir * kickBack, ForceMode.Impulse);

            Instantiate(bloodHitFX, _BulletPrefab.position, _BulletPrefab.rotation);  //Instatiating straight away even if no collision with "Zombie"

            Destroy(gameObject);

        }
        if (isDeagle == true && collision.gameObject.tag == "Zombie")
        {
            ZombieAI zomb = collision.gameObject.GetComponent<ZombieAI>();

            zomb.gameObject.SendMessage("TakeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);
            zomb.isHit = true;

            Vector3 shotDir = zomb.transform.position - transform.position;
            shotDir.y = 0;

            zomb.rb.AddForce(shotDir * kickBack, ForceMode.Impulse);

            Instantiate(bloodHitFX, _BulletPrefab.position, _BulletPrefab.rotation); //Instatiating straight away even if no collision with "Zombie"
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


