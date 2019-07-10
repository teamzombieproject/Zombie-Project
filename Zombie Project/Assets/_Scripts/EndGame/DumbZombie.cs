using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DumbZombie : MonoBehaviour
{
    Rigidbody rb;
    public int neg = 1;
    float randomY, randomX;
    int random = 1;
    public AudioClip[] audioClips;
    float timer = 0, pastTime = 0;
    void Start()
    {
        GetComponentInChildren<Animator>().speed = 0.4f;
        rb = GetComponent<Rigidbody>();
        neg = Random.Range(0, 2) * 2 - 1;
        randomX = Random.Range(0.2f, 2f) * neg;
        randomY = Random.Range(0.2f, 2f) * neg; 
        if (randomX > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        if (randomY > 0)
        {
            GetComponentInChildren<Animator>().SetBool("back", true);
        }
        pastTime = Random.Range(3, 20);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        rb.velocity = new Vector3(randomY, 0, randomX);

        if (timer > pastTime)
        {
            GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
            GetComponent<AudioSource>().Play();
            pastTime = Random.Range(10, 50);
        }
        
    }
}
