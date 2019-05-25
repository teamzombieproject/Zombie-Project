using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
    public float destroyTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyClone());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator DestroyClone()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
