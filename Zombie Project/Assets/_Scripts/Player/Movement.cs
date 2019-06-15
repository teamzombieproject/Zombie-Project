using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    float horMovementInputValue;
    float vertMovementInputValue;
    public float speed = 10, knockbackSlowdown = 6;
    public Vector3 hitKnockback;
    public int directionX = 0;
    public int directionZ = 0;
    public float walkTimerInitial = .5f;
    float speedRegulator = 1, walkTimer = .5f;
    bool walkPitch = false;
    public AudioClip footstep;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        walkTimer = walkTimerInitial;
    }


    void Update()

    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
           directionZ = 1;
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            directionZ = -1;
        else
            directionZ = 0;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            directionX = -1;
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            directionX = 1;
        else
            directionX = 0;

        if (directionZ != 0 && directionX != 0)
            speedRegulator = .7f;
        else speedRegulator = 1;

        

        if (walkTimer > 0 && (directionX != 0 || directionZ != 0))
        {
            walkTimer -= Time.deltaTime;
        }
        else if (walkTimer <= 0 && !GetComponent<AudioSource>().isPlaying)
        {
            walkTimer = walkTimerInitial;
            
            GetComponent<AudioSource>().clip = footstep;
            
            GetComponent<AudioSource>().Play();
            if (walkPitch)
            {
                GetComponent<AudioSource>().pitch = 1 + Random.Range(.1f, .3f);
            }
            else
            {
                GetComponent<AudioSource>().pitch = 1 - Random.Range(0, .2f);
            }
            walkPitch = !walkPitch;

        }

    }
    private void FixedUpdate()
    {
        if (hitKnockback.x > 0)
        {
            hitKnockback -= (Vector3.one * Time.deltaTime * knockbackSlowdown + (hitKnockback / 100));
        }
        if (hitKnockback.x < 0) hitKnockback = Vector3.zero;

        //transform.position += transform.forward * speed * -directionZ * speedRegulator * Time.deltaTime + transform.right * speed * -directionX * speedRegulator * Time.deltaTime;
        playerRigidbody.velocity = new Vector3(-directionX + hitKnockback.x, 0, -directionZ + hitKnockback.z) * speed * speedRegulator;

    }
}
