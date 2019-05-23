using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingLegacy : MonoBehaviour
{
    LayerMask layerMask;
    float angleY = 0;
    int direction = 1;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Ground"); // gets ground layer for use with rays
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))//if a ray is cast and hits layermask (outputting the raycast 'hit' as a variable)
        {
            Vector3 targetDir = hit.point - transform.position;

            if (Camera.main.WorldToScreenPoint(transform.position).x <= Input.mousePosition.x) //flips gameobject for aesthetics    
            { angleY = 180; direction = -1; }
            else { angleY = 0; direction = 1; }

            //find the angle from the player to the mouse using trig and convert it to degrees
            float angle = Mathf.Atan2(targetDir.z, targetDir.x) * Mathf.Rad2Deg; 
            transform.rotation = Quaternion.Euler(-45 + angleY, 0, 90) * Quaternion.AngleAxis(angle * -direction, Vector3.forward); // apply rotation to game object
        }
    }
}
