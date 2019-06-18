﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public float radioHealth;
    public List<ZombieAI> zombieAIScript;
    Image radioHealthBar;
    float maxHealth = 100f;
    public GameManager GM;


    private void Start()
    {
        radioHealth = maxHealth;
        radioHealthBar = GameObject.FindGameObjectWithTag("RadioHealthBar").GetComponent<Image>();
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (radioHealth <= 0)  // make this game over instead of script below
        {
            Debug.Log("destroy me");
            for (int i=0; i<zombieAIScript.Count; i++)
            {
               // zombieAIScript[i].radioBeingAttacked = false;
                zombieAIScript[i].attackObject = null;
            }
            GM.isRadioDead = true;
            Destroy(this.gameObject); 
        }

        radioHealthBar.fillAmount = radioHealth / maxHealth;
    }

    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && GM.bEPiecePickedUp)
        {
            GM.bEPieces += 1;
            GM.bEPiecePickedUp = false;
        }
    }
    
}


