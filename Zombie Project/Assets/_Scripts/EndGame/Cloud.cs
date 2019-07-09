using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    Rigidbody rb;
    public bool negative;
    int neg;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (negative)
            neg = -1;
        else
            neg = 1;
        rb.velocity = new Vector3(Random.Range(5, 40) * neg, 0, 0);
    }
}
