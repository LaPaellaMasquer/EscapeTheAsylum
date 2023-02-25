using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string tag;

    private void Awake()
    {
        GameObject[] levels = GameObject.FindGameObjectsWithTag(tag);

        if (levels.Length > 1)
        {
            DestroyImmediate(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
