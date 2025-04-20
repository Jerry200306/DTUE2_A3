using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class SceneLoader : MonoBehaviour
{

    public string nextSceneName = "Scene2";

    public void LoadNextScene()
    {

SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}