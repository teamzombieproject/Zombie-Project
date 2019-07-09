using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{
    Rigidbody rb;
    Vector3 initialPos,initPosObj;
    float x = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.GetChild(0).localPosition;
        initPosObj = transform.position;

    }
    void Update()
    {
        x += Time.deltaTime * 2;
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(rb.velocity.x / 100, 0, 1) * -22);
        rb.velocity = Vector3.right * 250;
        transform.GetChild(0).transform.localPosition = initialPos + Random.insideUnitSphere * (2f);
        transform.position = new Vector3(transform.position.x, Mathf.Sin(x)* 50 + initPosObj.y, transform.position.z);
    }
}
