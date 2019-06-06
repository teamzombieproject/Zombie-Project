using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    //to activate screenshake, do the following line of code:
    //*MainCamera(gameobject)*.GetComponent<ScreenShake>().CamShake(*magnitude(float)*, *length(float)*);
    //replace asterisk'd things with variables.

    Vector3 cameraInitial;
    public GameObject mainCamera; //should just be gameObject but 
    float shakeMagnitude, shakeLength, lengthInitial; // shake mag is radius of sphere (used to find a random vector), length is a timer, lengthInitial is so i can make length a percentage;
    public float magDefault = 1, lengthDefault = 1;
    bool shakeOn = false;

    public bool shake;
    void Start()
    {
        cameraInitial = mainCamera.transform.localPosition;
    }

    private void Update()
    {
        if (shake)
        {
            CamShake(magDefault,lengthDefault);
            shake = false;
        }
        if (shakeOn)
        {
            
            shakeLength -= Time.deltaTime;

            transform.localPosition = cameraInitial + Random.insideUnitSphere * (shakeMagnitude/10) * (shakeLength/lengthInitial);
            
            if (shakeLength <= 0)
            {
                shakeLength = 0;
                shakeOn = false;
                transform.localPosition = cameraInitial;
            }
        }
    }

    public void CamShake(float magnitude, float length)
    {
        if (magnitude > 0)
            shakeMagnitude = magnitude;
        else shakeMagnitude = magDefault;
        if (length > 0)
        {
            shakeLength = length;
            lengthInitial = length;
        }
        else
        {
            shakeLength = lengthDefault;
            lengthInitial = lengthDefault;
        }
        shakeOn = true;
    }
    
}
