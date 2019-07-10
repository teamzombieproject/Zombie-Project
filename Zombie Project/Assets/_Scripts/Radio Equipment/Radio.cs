using System.Collections;
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
    public Text radioUnderAttack;
    public Renderer rend;

    public Material standard;
    public Material transparent;



    private void Start()
    {
        radioHealth = maxHealth;
        radioHealthBar = GameObject.FindGameObjectWithTag("RadioHealthBar").GetComponent<Image>();
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        radioUnderAttack = GameObject.Find("Radio Under Attack").GetComponent<Text>();
        radioUnderAttack.text = "";
      

    }

    // Update is called once per frame
    void Update()
    {
        if (radioHealth <= 0)  // make this game over instead of script below
        {
            Debug.Log("destroy me");
            for (int i = 0; i < zombieAIScript.Count; i++)
            {
                if (zombieAIScript[i] != null)
                    zombieAIScript[i].attackObject = null;
            }
            GM.isRadioDead = true;
            Destroy(gameObject);
        }

        radioHealthBar.fillAmount = radioHealth / maxHealth;

        if (zombieAIScript.Count > 0 && GM.actionPhaseActive == true)
        {
            radioUnderAttack.text = "Radio Under Attack!!";
           
            foreach(ZombieAI zombie in zombieAIScript)
            {
                if (zombie == null)
                {
                    zombieAIScript.Remove(zombie);
                }
            }
        }

        else
        {
            radioUnderAttack.text = "";
        }

    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && GM.bEPiecePickedUp)
        {
            //GM.BEUpdateText.text = "Goal: Get the BE Drop. It has spawned somewhere around the map.";
            GM.BEUpdateText.text = "Goal: Kill the rest of the zombies.";
            GM.bEPieces += 1;
            GM.bEPiecePickedUp = false;
            GetComponent<AudioSource>().Play();
            OffScreenPoint[] offPoint = collision.gameObject.GetComponentsInChildren<OffScreenPoint>();
            for (int i = 0; i < offPoint.Length; i++)
            {
                if (offPoint[i] != null)
                {
                    if (offPoint[i].isGoalArrow)
                    {
                        offPoint[i].pointTransform = null;
                    }
                }
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "Player")
        {

            rend.material = transparent;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "Player")
        {
            rend.material = standard;
        }

    }

}
