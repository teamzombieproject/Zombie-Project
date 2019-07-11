using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
   public GameManager GM;
    public AudioSource audioSrc;
    public AudioClip activeMusic;
    public AudioClip buildMusic;
    bool activeIsPlaying = false;
    bool buildIsPlaying = false;

   
    void Start()
    {
       GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSrc = GetComponent<AudioSource>();
            audioSrc.clip = activeMusic;
            audioSrc.Play();
        
    }

   
    void Update()
    {
        if (GM.actionPhaseActive == true && activeIsPlaying == false)
        {
            PlayActiveMusic();
            activeIsPlaying = true;
            buildIsPlaying = false;
           
        }
        else if (GM.actionPhaseActive == false && buildIsPlaying == false)
       {
            PlayBuildMusic();
            activeIsPlaying = false;
            buildIsPlaying = true;

         }

    }

   void  PlayActiveMusic()
    {
        audioSrc.clip = activeMusic;
        audioSrc.Play();
    }

   void  PlayBuildMusic()
    {
        audioSrc.clip = buildMusic;
        audioSrc.Play();
    }
}
