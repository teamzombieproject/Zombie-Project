using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDrop : MonoBehaviour
{
    public GameManager GM;

    private void Start()
    {
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
