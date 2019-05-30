using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScreen : MonoBehaviour
{
    //Apply on sprite to make it point at screen
    //make sure the sprite is angled flat to the floor with it's face pointing up
    
    void Start()
    {
        transform.Rotate(45, 0, 0, Space.World);
    }

  
}
