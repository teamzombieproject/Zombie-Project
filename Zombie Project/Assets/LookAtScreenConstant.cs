using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScreenConstant : MonoBehaviour
{
    public bool needsToRotate = true;

    // Update is called once per frame
    void Update()
    {
        if (needsToRotate)
        {
            transform.Rotate(45, 0, 0, Space.World);
        }
    }


}
