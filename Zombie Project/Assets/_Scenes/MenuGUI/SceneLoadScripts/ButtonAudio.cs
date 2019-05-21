using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnClick()
    {
        audioSource.Play();
    }

}
