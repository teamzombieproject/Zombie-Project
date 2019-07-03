using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteNotify : MonoBehaviour
{
    public void DeathEnd()
    {

        GetComponentInParent<Health>().deathIsFinished = true;
        transform.parent.Find("ShadowCylinder").gameObject.SetActive(false);
        GetComponent<Animator>().enabled = false;
    }
}
