using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimplane : MonoBehaviour
{
    public GameObject arm;
    void Update()
    {
        transform.position = arm.transform.position;
    }
}
