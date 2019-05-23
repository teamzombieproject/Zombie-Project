using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    float horMovementInputValue;
    float vertMovementInputValue;
    public float m_Speed = 10;
    Vector3 movement;
    Vector3 movement2;
    int directionX;
    int directionZ;
    float speedRegulator = 1;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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



        
    }
    private void FixedUpdate()
    {
        transform.position += transform.forward * m_Speed * -directionZ * speedRegulator * Time.deltaTime + transform.right * m_Speed * -directionX * speedRegulator * Time.deltaTime;
    }
}
