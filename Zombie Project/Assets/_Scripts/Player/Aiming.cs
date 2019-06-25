using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Camera cameraObject;

    private void Start()
    {
        if (cameraObject == null) cameraObject = Camera.main;
    }
    private void LateUpdate() // on lateupdate instead of fixed update. know its not the best for fairness but it looks so much better
    {
        Aim(); 
    }

    void Aim()
    {
        Ray cameraObjectRay = cameraObject.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraObjectRay, out rayLength))
        {
            Vector3 pointToLook = cameraObjectRay.GetPoint(rayLength);
            Debug.DrawLine(cameraObjectRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
