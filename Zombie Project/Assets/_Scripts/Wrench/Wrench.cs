using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    public float forceMagnitude = 10, damage = 30, initialCooldown = 3;
    float cooldown = 0;
    bool attacking;
    int rotation = 1, count = 0;
    public int swingSpeed = 6;
    public GameObject wrenchSprites;
    
    AudioSource wrenchSFX;
    private void Start()
    {
        wrenchSFX = GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown <= 0)
        {
            if (!attacking)
            {
                cooldown = initialCooldown;
                attacking = true;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                Camera.main.GetComponent<ScreenShake>().CamShake(1f, .5f);
                wrenchSFX.Play();
            }
                
            

        }
        
    }

    private void FixedUpdate()
    {
        if (attacking)
        {
            Swipe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            Debug.Log("Zombie Hit");
                other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * forceMagnitude, ForceMode.VelocityChange);
                other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                //hitSFX.Play();
        }
    }

    void Swipe()
    {
        
        if (rotation == 1 && count < swingSpeed)
        {
            count++;
            wrenchSprites.transform.localRotation = Quaternion.Euler(0, -60 / (swingSpeed / 2) * (swingSpeed / 2 - count), 0);
        }
        else if (count == swingSpeed && rotation == 1 && attacking)
        {
            rotation = -1;
            Debug.Log(rotation);
            attacking = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            count = 0;
        }

        if (rotation == -1 && count < swingSpeed && attacking)
        {
            count++;
            wrenchSprites.transform.localRotation = Quaternion.Euler(0, 60 / (swingSpeed/2) * (swingSpeed / 2 - count), 0);
        }
        else if (count == swingSpeed && rotation == -1)
        {
            rotation = 1;
            Debug.Log(rotation);
            attacking = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            count = 0;
        }


    }
}
