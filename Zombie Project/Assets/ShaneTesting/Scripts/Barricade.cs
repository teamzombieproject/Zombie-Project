using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{

    public int barricadeHealth;
    

    // Update is called once per frame
    void Update()
    {
        if (barricadeHealth <=0)
        {
            Destroy(this.gameObject,3);
        }
    }
}
