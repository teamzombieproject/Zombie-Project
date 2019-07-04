using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellScript : MonoBehaviour
{
    public float rotSpeed = 2000f;
    bool canRotate = true;
    Rigidbody rb;
    public float force = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(force, force, 0), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + (rotSpeed * Time.deltaTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlaceableGround")
        {
            canRotate = false;
            rotSpeed = 0;
            rb.velocity = Vector3.zero;
        }
    }
}
