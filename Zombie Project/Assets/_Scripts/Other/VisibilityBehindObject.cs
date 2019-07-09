using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityBehindObject : MonoBehaviour
{
    Renderer rend;
    public Material standardMaterial;
    public Material transparentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float halfSize = rend.bounds.size.x / 2;
        float objectHeight = rend.bounds.size.y/2;
        float yBuffer = 0.1f;

        Vector3 closeEdgeOfObject = new Vector3(transform.position.x, transform.position.y + yBuffer, transform.position.z - rend.bounds.size.z);
        Vector3 centerOfObject = new Vector3(transform.position.x + halfSize, transform.position.y + yBuffer, transform.position.z - rend.bounds.size.z);
        Vector3 farEdgeOfObject = new Vector3(transform.position.x + rend.bounds.size.x, transform.position.y + yBuffer, transform.position.z - rend.bounds.size.z);

        if(Physics.Raycast(closeEdgeOfObject, -transform.forward, objectHeight) ||
           Physics.Raycast(centerOfObject, -transform.forward, objectHeight) ||
           Physics.Raycast(farEdgeOfObject, -transform.forward, objectHeight))
        {
            Debug.DrawRay(closeEdgeOfObject, -transform.forward * objectHeight, Color.red);
            Debug.DrawRay(centerOfObject, -transform.forward * objectHeight, Color.red);
            Debug.DrawRay(farEdgeOfObject, -transform.forward * objectHeight, Color.red);

            SetTransparentMat();
        }
        else
        {
            SetStandardMat();
        }
    }

    public void SetTransparentMat()
    {
        rend.material = transparentMaterial;
    }

    public void SetStandardMat()
    {
        rend.material = standardMaterial;
    }
}
