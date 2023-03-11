using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    static bool isFirst;

    private void Awake()
    {
        
        if (isFirst)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        isFirst = true;
    }

    private void SceneManager_activeSceneChanged(Scene current, Scene next)
    {
        gameObject.SetActive(next.name == gameObject.tag);
    }
}
