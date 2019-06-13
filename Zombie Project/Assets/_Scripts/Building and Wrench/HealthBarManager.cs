using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    Image[] healthbars;
    GameObject[] repairables;
    string[] gameObjectTags;
    ArrayList arrayList;
    

    private void OnEnable()
    { 
        
    }

    public void OnDisable() //I just found function what the hell where was this this whole time
    {
       
    }


    void FindGameObjects()
    {
        List<GameObject[]> repairables = new List<GameObject[]>();
        for (int i = 0; i < gameObjectTags.Length; i++)
        {

        }





    }
}
