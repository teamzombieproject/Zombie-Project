using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    // CODE FOR ALL HAND WEAPONS //
    // SEE NOTES BELOW           //

    // Create diffrent bullet prefabs for each weapon and attach this script and fill out the inspector values. //

    // Damage dealt to zombies should be in the zombies health script "TakeDamage" //

    // Zombie prefabs/models in the Hieracrchy should be tagged "Zombie" //


    public int projectileDamage = 25;       // Damge of projectile = HANGDUN/.22 RIFLE/SEMI AUTO RIFLE/HIGH POWER RIFLE//SHOTGUN
    public float ProjectileSpeed = 2f;      // Speed/Delay of projectile
    public float destroyProjectile = 2f;    // Time/Range before destroy is called for projectile


    private Transform myTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.position += Time.deltaTime * ProjectileSpeed * myTransform.forward;

        destroyProjectile -= Time.deltaTime;

        if(destroyProjectile <= 0)
        {
            Destroy(gameObject);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.SendMessage("TakeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);
        }
    }
}


