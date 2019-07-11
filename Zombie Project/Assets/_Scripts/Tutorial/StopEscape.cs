using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEscape : MonoBehaviour
{
    public bool start = true;
    public BoxCollider enableWall, endWall, triggerWall;
    GameObject trap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enableWall.enabled = true;
            triggerWall.enabled = true;
        }
        if (other.gameObject.tag == "Traps")
        {
            Destroy(gameObject);
            Destroy(endWall.gameObject);
        }
    }

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
       trap = GameObject.FindGameObjectWithTag("Traps");
        if (trap != null)
        {

            Destroy(gameObject);
            Destroy(endWall.gameObject);
        }
    }
}
