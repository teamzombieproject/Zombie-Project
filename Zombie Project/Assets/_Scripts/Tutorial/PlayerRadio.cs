using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRadio : MonoBehaviour
{
    public int pickedUp = 0, returned = 0;
    public GameObject pickupFX, wall;
    public Text radioText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Radio")
        {
            if (pickedUp > 0)
            {
                pickedUp--;
                Instantiate(pickupFX, transform.position + new Vector3(2.5f, 0, 2.5f), Quaternion.Euler(-45, 0, 0));
                returned++;
                radioText.text = "BE Pieces: " + returned +"/5";
                if (returned >= 5)
                {
                    Destroy(wall);
                }
            }
        }
    }
}
