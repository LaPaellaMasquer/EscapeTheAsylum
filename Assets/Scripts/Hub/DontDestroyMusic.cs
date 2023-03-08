using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] levels = GameObject.FindGameObjectsWithTag(gameObject.tag);
        DontDestroyOnLoad(gameObject);
    }
}
