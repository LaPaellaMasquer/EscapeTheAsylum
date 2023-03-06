using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] levels = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (levels.Length > 1)
        {
            DestroyImmediate(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene current, Scene next)
    {
        gameObject.SetActive(next.name == gameObject.tag);
    }
}
