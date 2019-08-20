using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void OnClick(int sceneToLoad)
    {
        Debug.Log("Now loading scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
