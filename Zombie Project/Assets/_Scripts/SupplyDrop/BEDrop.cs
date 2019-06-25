using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDrop : MonoBehaviour
{
    public GameManager GM;

    private void Start()
    {
        //Connor Fettes added code to work with an arrow that points at BEDrop when it's off screen here
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OffScreenPoint>().pointTransform = gameObject.transform;
        //code done
        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GM.bEPiecePickedUp = true;
            Destroy(gameObject);
        }
    }
}
