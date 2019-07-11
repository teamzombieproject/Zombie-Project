using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathBlocker : MonoBehaviour
{
    float x = Mathf.PI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (x < Mathf.PI * 1.5f)
        {
            x += Time.deltaTime;

            GetComponent<Image>().fillAmount = Mathf.Cos(x + (Mathf.PI));
        }
    }
}
