using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCode : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
