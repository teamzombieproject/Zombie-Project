using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rtoreload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).GetComponent<Text>().text = "while holding shift, left click the turret with the wrench to repair";
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);

        }
    }
}
