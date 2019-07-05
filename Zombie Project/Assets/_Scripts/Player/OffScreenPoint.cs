using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenPoint : MonoBehaviour
{
    public Camera cameraObject;
    public Transform pointTransform;
    GameObject child;
    public Vector3 initialPos;
    bool point = false, isDisabled = false;
    float x = 0;
   

    private void Start()
    {
        if (cameraObject == null) cameraObject = Camera.main;
        child = gameObject.transform.GetChild(0).gameObject;
        initialPos = child.transform.localPosition;
    }
  
    private void LateUpdate()
    {
        
        if (pointTransform != null)
        {
            if (OffScreen())
            {
                Aim();
            }
            else if (!isDisabled)
            {
                Disable();
            }
        }
        else
            Disable();
    }

    void Aim()
    {
        if (point)
        {
            x += 4 * Time.deltaTime;
            if (x > 40 * Mathf.PI / 2)
                x = 0;
            UpDownMove();

            Vector2 pos = cameraObject.WorldToScreenPoint(pointTransform.position);
            Ray cameraObjectRay = cameraObject.ScreenPointToRay(pos);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(cameraObjectRay, out float rayLength))
            {
                Vector3 pointToLook = cameraObjectRay.GetPoint(rayLength);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }

    bool OffScreen()
    {
        Vector3 screenPos = cameraObject.WorldToViewportPoint(pointTransform.position);
        if (screenPos.x > 1 || screenPos.y > 1 || screenPos.x < 0 || screenPos.y < 0)
        {
            if (isDisabled)
            {
                child.SetActive(true);
                isDisabled = false;
                point = true;
            }
            return true;
        }
        Disable();
        return false;
    }

    void UpDownMove()
    {
        child.transform.localPosition = initialPos + new Vector3 (0,0,Mathf.Sin(x - Mathf.PI / 2))/4;
    }
    private void OnEnable()
    {
        if (cameraObject == null) cameraObject = Camera.main;

    }
    private void Disable()
    {
        point = false;
        child.SetActive(false);
        isDisabled = true;
    }
}
