using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    public string sceneName = string.Empty;

    public void OnButtonPressed()
    {
        if (sceneName == "Tutorial")
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().tutorialButtonPressed = true;
        SceneManager.LoadScene(sceneName);
    }
}