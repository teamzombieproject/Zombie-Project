using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    float horMovementInputValue;
    float vertMovementInputValue;
    public float m_Speed = 10;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        vertMovementInputValue = Input.GetAxis("Vertical");
        horMovementInputValue = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * -vertMovementInputValue * m_Speed * Time.deltaTime;
        Vector3 movement2 = transform.right * -horMovementInputValue * m_Speed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + movement + movement2);
    }
}
