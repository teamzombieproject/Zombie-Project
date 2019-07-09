using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDrop : MonoBehaviour
{
    public GameManager GM;
    public ParticleSystem pickupEffects;

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
            GM.BEUpdateText.text = "Goal: Bring the BE Drop back to the radio";
            Instantiate(pickupEffects, transform.position + new Vector3 (2.5f,0,2.5f), Quaternion.Euler(-45, 0, 0));
        }
    }
}
