using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //total health, invulnurability sprite flash speed, how long your invulnurable (s), how long you have to wait after taking damage to heal (s), how much you heal
    public float health = 100, invulnurability = 5, healWait = 10, healAmount = 1;
    //DONT MAKE INVULNURABILITY TIME NEGATIVE, PLAYER WILL BE INDESTRUCTABLE FOREVER.
    float timer = 0, healthTimer = 0, rotate = 0, timerInitial, startingHealth, multiplier = .1f;
    bool dead = false;
    public SpriteRenderer[] characterSprites;
    public Animator bodyAnimator, headAnimator;
    public GameObject charHead;
    public ChangeSprite changeSprite;
    public Movement movement;
    public Aiming aiming;
    public Light deathLight;
    public GameObject[] lights;
    bool damaged = false;
    public bool deathIsFinished = false;
    public AudioClip damageSFX;

    public bool takeDamage;
    private void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
        startingHealth = health;
        /* Debug.Log("starting health is " + startingHealth);
        Health[] test = GetComponents<Health>(); */
    }
    private void Update()
    {
        if (takeDamage)
        {
            TakeDamage(10,Vector3.zero);
            takeDamage = false;
            Debug.Log(takeDamage);
        }
        
        if (health <= 0)
        {
            OnDeath();
            dead = true;
        }

        Heal();

        //sprite flashing
        if (damaged)
        {
            timer += Time.deltaTime;
            multiplier = 5*timer;
            for (int i = 0; i < characterSprites.Length; i += 1)
            {
                characterSprites[i].color = new Color(1, (Mathf.Sin(multiplier + Mathf.PI / 2) + 3)/4, (Mathf.Sin(multiplier + Mathf.PI / 2) + 3)/4);
            }

            if (timer >= invulnurability)
            {
                damaged = false;
                timer = 0;
                multiplier = 0;
                for (int i = 0; i < characterSprites.Length; i += 1)
                {
                    characterSprites[i].color = Color.white;
                }
            }
        }
    }

    public void TakeDamage(float damage, Vector3 damagePosition)
    {
        
        if (damaged || health <= 0) //indestructible if damaged
            return;
        healthTimer = 0;
        health -= damage;
        movement.hitKnockback = gameObject.transform.position - damagePosition; //knockback
        Camera.main.GetComponent<ScreenShake>().CamShake(3.5f, .7f);
        GetComponent<AudioSource>().pitch = 1;
        GetComponent<AudioSource>().clip = damageSFX;
        GetComponent<AudioSource>().Play();
       
        if (health > 0)
        {
            timer = 0;
            //sprite flashing and indestructability
            damaged = true;
        }
        //GameObject.FindGameObjectWithTag("Healthbar").GetComponent<Image>()
        FindObjectOfType<Canvas>().transform.GetChild(0).Find("HealthBarBackground").Find("HealthBar").GetComponent<Image>().fillAmount = health / startingHealth;
    }

    void OnDeath()
    {
        // things that go repeatedly or after an amount of time go first
        if (deathLight.intensity <= 2.2f)
        {
            deathLight.intensity += .01f;
        }
        /*if (Camera.main.gameObject.GetComponent<CameraFollow>().radius >= 0.1f)
        {
            Camera.main.gameObject.GetComponent<CameraFollow>().radius -= 0.1f;
        }*/
        
        timer += Time.deltaTime;
        if (timer >= 2 && timer <= 2.5 && bodyAnimator.GetInteger("State") != 5)
        {

            bodyAnimator.SetInteger("State", 5);
            charHead.SetActive(false);
        }
        else if (timer > 2.5 && bodyAnimator.GetInteger("State") != 80)
        {
            bodyAnimator.SetInteger("State", 80);
            //activiate deathGUI()  <--------------- DO!!!!
            deathIsFinished = true;
        }

        
        if (dead)
            return;
        //things that go once after this
        Camera.main.gameObject.transform.parent = gameObject.transform;
        changeSprite.enabled = false; // stops sprite changing
        aiming.gameObject.SetActive(false);
        movement.enabled = false;
        headAnimator.SetInteger("Direction", 0);

        /*for (int i = 0; i < lights.Length; i += 1)
        {
            lights[i].GetComponent<Light>().intensity = .5f;
        }*/

        dead = false;



    }

    void Heal() //heal a certain amount of time after taking damage
    {
        if (health == startingHealth)
            return;
        //Debug.Log("healing, timer at " +healthTimer );
        healthTimer += Time.deltaTime;
        if (healthTimer >= healWait && health > 0)
        {
            healthTimer -= 1.0f;
            health += healAmount;
            //Debug.Log(health);
        }
    }
}
