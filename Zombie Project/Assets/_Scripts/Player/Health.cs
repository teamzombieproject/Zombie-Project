using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    float timer = 0, rotate = 0;
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
    public float multiplier = .1f, invulnurability = 5;
    //DONT MAKE INVULNURABILITY TIME NEGATIVE, IT WILL BE INDESTRUCTABLE FOREVER.

    public bool takeDamage;
    private void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
        
    }
    private void Update()
    {
        if (takeDamage)
        {
            TakeDamage(10);
            takeDamage = false;
        }

        if (health <= 0)
        {
            OnDeath();
            dead = true;
        }

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

    public void TakeDamage(float damage)
    {
        if (!damaged) //indestructible if damaged
            health -= damage;
        // inform health GUI
        if (health > 0)
        {
            timer = 0;
            //sprite flashing and indestructability
            damaged = true;
        }
    }

    void OnDeath()
    {
        // things that go repeatedly or after an amount of time go first
        if (deathLight.intensity <= 2.2f)
        {
            deathLight.intensity += .01f;
        }
        if (Camera.main.gameObject.GetComponent<CameraFollow>().radius >= 0.1f)
        {
            Camera.main.gameObject.GetComponent<CameraFollow>().radius -= 0.1f;
        }
        if (rotate < 365 && gameObject.GetComponentInChildren<Camera>() != null)
        {
            rotate += 5;
            gameObject.transform.Rotate(0, 5, 0);
        }
        else if (rotate >= 365)
            gameObject.transform.rotation = Quaternion.identity;

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
        }

        
        if (dead)
            return;
        //things that go once after this
        Debug.Log("THE OTHER SIDE");
        Camera.main.gameObject.transform.parent = gameObject.transform;
        changeSprite.enabled = false; // stops sprite changing
        aiming.enabled = false;
        headAnimator.SetInteger("Direction", 0);

        for (int i = 0; i < lights.Length; i += 1)
        {
            lights[i].GetComponent<Light>().intensity = .5f;
        }

        dead = false;



    }
}
