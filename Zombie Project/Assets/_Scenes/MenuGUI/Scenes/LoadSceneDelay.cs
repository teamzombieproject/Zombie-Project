using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDelay : MonoBehaviour
{
    IEnumerator SceneDelay(string sceneName = "Credits", float delay = 3f)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void OnClick()
    {
        StartCoroutine(SceneDelay("Credits"));
    }

}
