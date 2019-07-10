using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
   
    public float zombieHealth = 100f, deathMagnitude = 5;
    public GameObject corpsePrefab;
    GameManager  gm;
    NavMeshAgent navAgent;
    float colourChange = -1;
    AudioSource audioSrc;
    public AudioClip zombieHurt;



    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.zombiesAlive++;
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (zombieHealth <= 0)
        {
            if (gm != null)
          gm.zombiesAlive--;
            GameObject corpse = Instantiate(corpsePrefab, transform.position + new Vector3 (0,.1f,0), Quaternion.identity);
            if (gm != null)
            gm.corpses.Add(corpse);
            if (GetComponent<NavMeshAgent>().velocity.x > 0)
                corpse.GetComponentInChildren<SpriteRenderer>().flipX = false;
            else corpse.GetComponentInChildren<SpriteRenderer>().flipX = true;
            //corpse.GetComponentInParent<Rigidbody>().velocity = navAgent.velocity * deathMagnitude;
            Destroy(gameObject);

        }


        if (colourChange >= 0)
        {
            colourChange++;
            if (colourChange > 1)
            {
                
                GetComponentInChildren<SpriteRenderer>().color = new Color((Mathf.Clamp((colourChange),0,72)+ 183)/ 255, (Mathf.Clamp(colourChange * 2.43055556f, 0,175)+ 80)/ 255, (Mathf.Clamp(colourChange * 2.43055556f, 0, 175) + 80));
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color(.72f, .31f, .31f);
            }
            if (colourChange >= 255)
            {
                colourChange = -1;
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }
    }
    public void TakeDamage (float amount)
    {
        zombieHealth -= amount;
        colourChange = 0;
        audioSrc.clip = zombieHurt;
        audioSrc.Play();
    }


    public void Hurt(float amount, Vector3 position, float pushMag)
    {
        zombieHealth -= amount;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.velocity += (gameObject.transform.position - position) * pushMag;
        colourChange = 0;
    }


}
